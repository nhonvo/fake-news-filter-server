using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Collections.Generic;
using System;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.Media;
using AutoMapper;

namespace FakeNewsFilter.Application.Catalog.NewsManage
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDBContext _context;

        private readonly IMapper _mapper;

        private readonly FileStorageService _storageService;

        public NewsService(ApplicationDBContext context, FileStorageService storageService, IMapper mapper)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
        }

        //Get All News
        public async Task<List<NewsViewModel>> GetAll()
        {
            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            var data = await query
               .Select(x => new NewsViewModel()
               {
                   NewsId = x.n.NewsId,
                   Name = x.n.Name,
                   TopicId = x.c.TopicId,
                   LabelTopic = x.c.Label,
                   Description = x.c.Description,
                   PostURL = x.n.PostURL,
                   Media = _mapper.Map<MediaViewModel>(x.n.Media),
                   Timestamp = x.n.Timestamp,
               }).ToListAsync();

            return data;
        }

        public async Task<List<NewsViewModel>> GetAllByTopicId(GetPublicNewsRequest request)
        {
            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };


            var data = await query
                .Select(x => new NewsViewModel()
                {
                    NewsId = x.n.NewsId,
                    Name = x.n.Name,
                    TopicId = x.c.TopicId,
                    LabelTopic = x.c.Label,
                }).ToListAsync();

            return data;
        }

        //Get All News In Topic (Limit 50)
        public async Task<List<NewsViewModel>> GetNewsInTopic(GetPublicNewsRequest request)
        {
            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            query = query.Where(t => request.TopicId == t.nit.TopicId);

            var data = await query
                .Select(x => new NewsViewModel()
                {
                    NewsId = x.n.NewsId,
                    Name = x.n.Name,
                    TopicId = x.c.TopicId,
                    LabelTopic = x.c.Label,
                    Description = x.c.Description,
                    PostURL = x.n.PostURL,
                    Media = _mapper.Map<MediaViewModel>(x.n.Media),
                    Timestamp = x.n.Timestamp,
                }).ToListAsync();

            return data;
        }

        //Create News
        public async Task<int> Create(NewsCreateRequest request)
        {
            var news = new News()
            {
                Name = request.Name,

                Description = request.Description,

                PostURL = request.PostURL,

                Timestamp = DateTime.Now
            };
           

            //Save Image on Host
            if (request.ThumbnailMedia != null)
            {
                news.Media = new Media()
                {
                    Caption = "Thumbnail Image",
                    DateCreated = DateTime.Now,
                    FileSize = request.ThumbnailMedia.Length,
                    PathMedia = await SaveFile(request.ThumbnailMedia),
                    Type = (Data.Enums.MediaType)request.Type,
                };
            }

            _context.News.Add(news);


            _context.NewsInTopics.Add(new NewsInTopics()
            {
                NewsId = news.NewsId,
                TopicId = request.TopicId
            });

            await _context.SaveChangesAsync();

            return news.NewsId;
        }

        //Delete News
        public async Task<int> Delete(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);

            if (news == null) throw new FakeNewsException($"Cannot find a News with Id: {newsId}");

            var media = _context.Media.Find(newsId);

            if (media != null && media.PathMedia != null)
                await _storageService.DeleteFileAsync(media.PathMedia);

            _context.News.Remove(news);

            return await _context.SaveChangesAsync();
        }

        public async Task<NewsViewModel> GetById(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);

            NewsViewModel result = null;

            if (news != null)
            {
                var topic = _context.NewsInTopics.Where(x => x.NewsId == newsId).FirstOrDefault();
                var labeltopic = _context.TopicNews.Find(topic.TopicId);
                var media = _context.Media.Where(x => x.MediaId == news.ThumbNews).FirstOrDefault();

                result = new NewsViewModel()
                {
                    NewsId = news.NewsId,
                    Name = news.Name,
                    Description = news.Description,
                    PostURL = news.PostURL,
                    Media = _mapper.Map<MediaViewModel>(media),
                    Timestamp = news.Timestamp,
                    TopicId = topic.TopicId,
                    LabelTopic = labeltopic.Label
                };
            }

            return result;
        }

        //Update News
        public async Task<int> Update(NewsUpdateRequest request)
        {
            var news_update = await _context.News.FindAsync(request.Id);

            if (news_update == null) throw new FakeNewsException($"Cannont find a news with Id is: {request.Id}");

            news_update.Name = request.Name;
            news_update.Description = request.Description;
            news_update.PostURL = request.SourceLink;

            //Save Image
            if (request.ThumbnailMedia != null || request.MediaLink != null)
            {
                var thumb = _context.Media.FirstOrDefault(i => i.MediaId == news_update.ThumbNews);

                thumb.FileSize = 0;

                if (thumb.PathMedia != null)
                {
                    await _storageService.DeleteFileAsync(thumb.PathMedia);
                    thumb.PathMedia = null;
                }
                if (request.ThumbnailMedia != null)
                {
                    thumb.FileSize = request.ThumbnailMedia.Length;
                    thumb.PathMedia = await SaveFile(request.ThumbnailMedia);
                }

                thumb.Type = request.Type;

                _context.Media.Update(thumb);
            }

            return await _context.SaveChangesAsync();
        }

        //Update Link News
        public async Task<bool> UpdateLink(int newsId, string newLink)
        {
            var news_update = await _context.News.FindAsync(newsId);

            if (news_update == null) throw new FakeNewsException($"Cannont find a news with Id is: {newsId}");

            news_update.PostURL = newLink;

            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}