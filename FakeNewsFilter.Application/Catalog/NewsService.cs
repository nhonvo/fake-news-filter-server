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

namespace FakeNewsFilter.Application.Catalog
{
    public interface INewsService
    {
        Task<ApiResult<List<NewsViewModel>>> GetAll(string language);

        Task<ApiResult<List<NewsViewModel>>> GetNewsInTopic(GetPublicNewsRequest request);

        Task<ApiResult<int>> Create(NewsCreateRequest request);

        Task<ApiResult<bool>> Delete(int NewsId);

        Task<ApiResult<NewsViewModel>> GetById(int newsId);

        Task<ApiResult<bool>> Update(NewsUpdateRequest request);

        Task<ApiResult<bool>> UpdateLink(int newsId, string newLink);
    }

    public class NewsService : INewsService
    {
        private readonly ApplicationDBContext _context;

        private readonly IMapper _mapper;

        private readonly FileStorageService _storageService;

        public NewsService(ApplicationDBContext context, FileStorageService storageService, IMapper mapper)
        {
            _context = context;
            FileStorageService.USER_CONTENT_FOLDER_NAME = "images/news";
            _storageService = storageService;
            _mapper = mapper;
        }

        //Get All News
        public async Task<ApiResult<List<NewsViewModel>>> GetAll(string language)
        {
            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            var list_news = await query.Where(t => string.IsNullOrEmpty(language) || t.n.LanguageCode == language)
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

            if(list_news == null)
            {
                return new ApiErrorResult<List<NewsViewModel>>("Get All News Unsuccessful!");
            }

            return new ApiSuccessResult<List<NewsViewModel>>("Get All News Successful!", list_news);
        }

        //Lấy thông tin News thông qua Id
        public async Task<ApiResult<NewsViewModel>> GetById(int newsId)
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

                return new ApiSuccessResult<NewsViewModel>("Get This News Successful!", result);
            }
            else
            {
                return new ApiErrorResult<NewsViewModel>("Get This News Unsuccessful!");
            }
        }


        //Lấy tất cả các tin tức có trong chủ đề
        public async Task<ApiResult<List<NewsViewModel>>> GetNewsInTopic(GetPublicNewsRequest request)
        {
                var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            query = query.Where(t => request.TopicId == t.nit.TopicId).Where(t => string.IsNullOrEmpty(request.LanguageCode) || t.n.LanguageCode == request.LanguageCode) ;

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

            if (data == null)
            {
                return new ApiErrorResult<List<NewsViewModel>>("Get All News In Topic Unsuccessful!");
            }

            return new ApiSuccessResult<List<NewsViewModel>>("Get All News In Topic Successful!", data);
        }

        //Tạo mới 1 tin tức
        public async Task<ApiResult<int>> Create(NewsCreateRequest request)
        {
            var news = new News()
            {
                Name = request.Name,

                Description = request.Description,

                PostURL = request.PostURL,

                DatePublished = request.DatePublished ?? DateTime.Now,

                Publisher = request.Publisher,

                LanguageCode = request.LanguageCode,

                Timestamp = DateTime.Now
            };
           

            //Save Image on Host
            if (request.ThumbNews != null)
            {
                news.Media = new Media()
                {
                    Caption = "Thumbnail Image",
                    DateCreated = DateTime.Now,
                    FileSize = request.ThumbNews.Length,
                    PathMedia = await SaveFile(request.ThumbNews),
                    Type = (Data.Enums.MediaType)request.Type,
                };
            }

            _context.News.Add(news);

            await _context.SaveChangesAsync();

            _context.NewsInTopics.Add(new NewsInTopics()
            {
                NewsId = news.NewsId,
                TopicId = request.TopicId
            });

            if(await _context.SaveChangesAsync() == 0)
            {
                await _storageService.DeleteFileAsync(news.Media.PathMedia);
                return new ApiErrorResult<int>("Create News Unsuccessful! Try again");
            }

            return new ApiSuccessResult<int>("Create News Successful!", news.NewsId);
        }

        //Xoá tin tức
        public async Task<ApiResult<bool>> Delete(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);

            if (news == null)
                return new ApiErrorResult<bool>($"Cannont find a news with Id is: {newsId}");

            var media = _context.Media.Find(news.ThumbNews);

            if (media != null && media.PathMedia != null)
            {
                await _storageService.DeleteFileAsync(media.PathMedia);
                _context.Media.Remove(media);
            }
            _context.News.Remove(news);

            if(await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("Delete News Unsuccessful! Try again");
            }

            return new ApiSuccessResult<bool>("Delete News Successful!", false);
        }

        
        //Cập nhật tin tức
        public async Task<ApiResult<bool>> Update(NewsUpdateRequest request)
        {
            var news_update = await _context.News.FindAsync(request.Id);

            if (news_update == null)
                return new ApiErrorResult<bool>($"Cannont find a news with Id is: {request.Id}");

            news_update.Name = request.Name ?? news_update.Name ;
            news_update.Description = request.Description ?? news_update.Description;
            news_update.PostURL = request.SourceLink ?? news_update.PostURL;

            //Save Image
            if (request.ThumbNews != null || request.MediaLink != null)
            {
                var thumb = _context.Media.FirstOrDefault(i => i.MediaId == news_update.ThumbNews);

                thumb.FileSize = 0;

                if (thumb.PathMedia != null)
                {
                    await _storageService.DeleteFileAsync(thumb.PathMedia);
                    thumb.PathMedia = null;
                }
                if (request.ThumbNews != null)
                {
                    thumb.FileSize = request.ThumbNews.Length;
                    thumb.PathMedia = await SaveFile(request.ThumbNews);
                }

                thumb.Type = request.Type;

                _context.Media.Update(thumb);
            }

            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("Update News Unsuccessful! Try again");
            }

            return new ApiSuccessResult<bool>("Update News Successful!", false);
        }

        //Update Link News
        public async Task<ApiResult<bool>> UpdateLink(int newsId, string newLink)
        {
            var news_update = await _context.News.FindAsync(newsId);

            if (news_update == null) 
                   return new ApiErrorResult<bool>($"Cannont find a news with Id is: {newsId}");

            news_update.PostURL = newLink;

            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("Update Link News Unsuccessful! Try again");
            }

            return new ApiSuccessResult<bool>("Update Link News Successful!", false);
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