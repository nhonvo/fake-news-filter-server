
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
    public class ManageNewsService : IManageNewsService
    {
        private readonly ApplicationDBContext _context;

        private readonly FileStorageService _storageService;

        private readonly IMapper _mapper;

        public ManageNewsService(ApplicationDBContext context, FileStorageService storageService, IMapper mapper)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
        }

        //Create News
        public async Task<int> Create(NewsCreateRequest request)
        {
            var news = new News()
            {
                Name = request.Name,

                Description = request.Description,

                SourceLink = request.SourceLink,

                Timestamp = DateTime.Now
            };
            _context.News.Add(news);
            await _context.SaveChangesAsync();

           
            //If exists MediaLink

            if (request.MediaLink != null)
            {
                news.Media = new Media()
                {
                    NewsId = news.NewsId,
                    Caption = "Thumbnail Image",
                    DateCreated = DateTime.Now,
                    Url = request.MediaLink,
                    Type = (Data.Enums.MediaType)request.Type,
                };
            };

            //Save Image on Host
            if (request.ThumbnailMedia != null)
            {
                news.Media =  new Media()
                {
                    NewsId = news.NewsId,
                    Caption = "Thumbnail Image",
                    DateCreated = DateTime.Now,
                    FileSize = request.ThumbnailMedia.Length,
                    PathMedia = await SaveFile(request.ThumbnailMedia),
                    Type = (Data.Enums.MediaType)request.Type,
                };
            }

            _context.NewsInTopics.Add(new NewsInTopics()
            {
                NewsId = news.NewsId,
                TopicId = request.TopicId
            });

            await _context.SaveChangesAsync();

            return news.NewsId;

        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        //Update News
        public async Task<int> Update(NewsUpdateRequest request)
        {
            var news_update = await _context.News.FindAsync(request.Id);

            if (news_update == null) throw new FakeNewsException($"Cannont find a news with Id is: {request.Id}");

            news_update.Name = request.Name;
            news_update.Description = request.Description;
            news_update.SourceLink = request.SourceLink;
           
            //Save Image
            if (request.ThumbnailMedia != null || request.MediaLink != null)
            {
                var thumb = _context.Media.FirstOrDefault(i => i.NewsId == request.Id);

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
                thumb.Url = request.MediaLink;
                
                _context.Media.Update(thumb);
            }

            return await _context.SaveChangesAsync();
        }


        //Update Link News
        public async Task<bool> UpdateLink(int newsId, string newLink)
        {
            var news_update = await _context.News.FindAsync(newsId);

            if (news_update == null) throw new FakeNewsException($"Cannont find a news with Id is: {newsId}");

            news_update.SourceLink = newLink;
          
            return await _context.SaveChangesAsync() > 0;
        }


        //Delete News
        public async Task<int> Delete(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);

            if (news == null) throw new FakeNewsException($"Cannot find a News with Id: {newsId}");


            var media = _context.Media.Find(newsId);

            if(media != null && media.PathMedia!=null) 
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
                var media = _context.Media.Where(x => x.NewsId == newsId).FirstOrDefault();
                result = new NewsViewModel()
                {
                    NewsId = news.NewsId,
                    Name = news.Name,
                    Description = news.Description,
                    SourceLink = news.SourceLink,
                    Media = _mapper.Map<MediaViewModel>(media),
                    Timestamp = news.Timestamp,
                    TopicId = topic.TopicId,
                    LabelTopic = labeltopic.Label
                };
            }

            return result;
        }
    }
}
