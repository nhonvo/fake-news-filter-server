﻿using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
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
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.Application.Catalog
{
    public interface INewsService
    {
        Task<ApiResult<List<NewsViewModel>>> GetAll(string languageId);

        Task<ApiResult<List<NewsViewModel>>> GetNewsInTopic(int topicId);

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

        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

        public NewsService(ApplicationDBContext context, FileStorageService storageService, IMapper mapper)
        {
            _context = context;
            FileStorageService.USER_CONTENT_FOLDER_NAME = "images/news";
            _storageService = storageService;
            _mapper = mapper;
        }

        //Get All News
        public async Task<ApiResult<List<NewsViewModel>>> GetAll(string languageId)
        {
            var language = await _context.Languages.SingleOrDefaultAsync(x => x.Id == languageId);
            
            var list_news = await _context.News.Where(n => !string.IsNullOrEmpty(languageId) ? n.LanguageId == languageId : true)
                .Select(x => new NewsViewModel()
               {
                   NewsId = x.NewsId,
                   Name = x.Name,
                   TopicInfo = x.NewsInTopics.Select(o => new TopicInfo { TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                   Description = x.Description,
                   Content = x.Content,
                   OfficialRating = x.OfficialRating,
                   Publisher = x.Publisher,
                   Status = x.Status,
                   ThumbNews = _mapper.Map<MediaViewModel>(x.Media),
                   LanguageId = x.LanguageId,
                   Timestamp = x.Timestamp,      
               }).ToListAsync();

            

            if (list_news == null)
            {
                return new ApiErrorResult<List<NewsViewModel>>("GetAllNewsUnsuccessful");
            }

            if (language == null)
            {
                return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsSuccessful", list_news);
            }

            return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsSuccessful", list_news);
        }

        //Lấy thông tin News thông qua Id
        public async Task<ApiResult<NewsViewModel>> GetById(int newsId)
        {
            var news = await _context.News.Include(t=>t.NewsInTopics).FirstOrDefaultAsync(t=>t.NewsId == newsId);

            NewsViewModel result = null;

            if (news != null)
            {
                var topic = news.NewsInTopics.Select(o => new TopicInfo { TopicId = o.TopicId, TopicName = _context.TopicNews.FirstOrDefault(m=>m.TopicId == o.TopicId).Tag}).ToList();

                var media = _context.Media.Where(x => x.MediaId == news.ThumbNews).FirstOrDefault();

                result = new NewsViewModel()
                {
                    NewsId = news.NewsId,
                    Name = news.Name,
                    Description = news.Description,
                    Content = news.Content,
                    OfficialRating = news.OfficialRating,
                    Publisher = news.Publisher,
                    ThumbNews = _mapper.Map<MediaViewModel>(media),
                    LanguageId = news.LanguageId,
                    Timestamp = news.Timestamp,
                    Status = news.Status,
                    TopicInfo = topic.ToList(),
                };

                return new ApiSuccessResult<NewsViewModel>("GetThisNewsSuccessful", result);
            }
            else
            {
                return new ApiErrorResult<NewsViewModel>("NewsIsNotFound");
            }
        }


        //Lấy tất cả các tin tức có trong chủ đề
        public async Task<ApiResult<List<NewsViewModel>>> GetNewsInTopic(int topicId)
        {
                var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            query = query.Where(t => topicId == t.nit.TopicId);

            var data = await query
                .Select(x => new NewsViewModel()
                {
                    NewsId = x.n.NewsId,
                    Name = x.n.Name,
                    LanguageId = x.n.LanguageId,
                    Description = x.n.Description,
                    Content = x.n.Content,
                    Status = x.n.Status,
                    ThumbNews = _mapper.Map<MediaViewModel>(x.n.Media),
                    Timestamp = x.n.Timestamp,
                }).ToListAsync();

            if (data == null)
            {
                return new ApiErrorResult<List<NewsViewModel>>("GetAllNewsInTopicUnsuccessful");
            }
            if (data.Count == 0)
            {
                return new ApiSuccessResult<List<NewsViewModel>>("DoNotHaveNewsInTopic");
            }
            return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsInTopicSuccessful", data);
        }

        //Tạo mới 1 tin tức
        public async Task<ApiResult<int>> Create(NewsCreateRequest request)
        {
            //check language
            var language = await _context.Languages.FirstOrDefaultAsync(x => x.Id == request.LanguageId);
            if(language == null)
            {
                return new ApiErrorResult<int>("LanguageNotFound");
            }
            //check topic id
            foreach (var item in request.TopicId)
            {
                var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                if (topic == null)
                {
                    return new ApiErrorResult<int>("TopicNotFound");
                }
            }
            var news = new News()
            {
                Name = request.Name,
                Description = request.Description,
                Content = request.Content,
                OfficialRating = request.OfficialRating,
                DatePublished = request.DatePublished ?? DateTime.Now,
                Publisher = request.Publisher,
                LanguageId = request.LanguageId,
                Timestamp = DateTime.Now
            };
           
            //Save Image on Host
            if (request.ThumbNews != null)
            {
                bool checkExtension = ImageExtensions.Contains(Path.GetExtension(request.ThumbNews.FileName).ToUpperInvariant());
                news.Media = new Media()
                {
                    
                    DateCreated = DateTime.Now,
                    FileSize = request.ThumbNews.Length,
                    PathMedia = await SaveFile(request.ThumbNews),
                    Type = checkExtension ? MediaType.Image : MediaType.Video,
                    Caption = "Thumb News " + (checkExtension ? "Image" : "Video")
                };
            }

            _context.News.Add(news);

            await _context.SaveChangesAsync();

            foreach(int topicId in request.TopicId)
            {
                _context.NewsInTopics.Add(new NewsInTopics()
                {
                    NewsId = news.NewsId,
                    TopicId = topicId
                });
            }    
            
            if(await _context.SaveChangesAsync() == 0)
            {
                await _storageService.DeleteFileAsync(news.Media.PathMedia);
                return new ApiErrorResult<int>("CreateNewsUnsuccessful");
            }

            return new ApiSuccessResult<int>("CreateNewsSuccessful", news.NewsId);
        }

        //Xoá tin tức
        public async Task<ApiResult<bool>> Delete(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);

            if (news == null)
                return new ApiErrorResult<bool>("CannontFindANewsWithId");

            if (news.ThumbNews != null)
            {
                var media = _context.Media.Single(x => x.MediaId == news.ThumbNews);

                if (media != null && media.PathMedia != null)
                {
                    await _storageService.DeleteFileAsync(media.PathMedia);
                    _context.Media.Remove(media);
                }
            }

            _context.News.Remove(news);

            if(await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("DeleteNewsUnsuccessful");
            }

            return new ApiSuccessResult<bool>("DeleteNewsSuccessful", false);
        }

        
        //Cập nhật tin tức
        public async Task<ApiResult<bool>> Update(NewsUpdateRequest request)
        {
            var news_update = await _context.News.FindAsync(request.Id);

            if (news_update == null)
                return new ApiErrorResult<bool>("CannontFindANewsWithId");

            news_update.Name = request.Name ?? news_update.Name ;
            news_update.Description = request.Description ?? news_update.Description;
            news_update.Content = request.Content ?? news_update.Content;
            news_update.LanguageId = request.LanguageId ?? news_update.LanguageId;

            if (request.ThumbNews != null)
            {
                //Kiểm tra hình đã có trên DB chưa
                var thumb = _context.Media.FirstOrDefault(i => i.MediaId == news_update.ThumbNews);

                //Nếu chưa có hình thì thêm hình mới
                if (thumb == null)
                {
                    news_update.Media = new Media()
                    {
                        Caption = "Thumbnail Topic",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbNews.Length,
                        PathMedia = await this.SaveFile(request.ThumbNews),
                        Type = MediaType.Image,
                        SortOrder = 1
                    };

                }
                else
                {
                    if (thumb.PathMedia != null)
                    {
                        await _storageService.DeleteFileAsync(thumb.PathMedia);
                    }
                    thumb.FileSize = request.ThumbNews.Length;
                    thumb.PathMedia = await SaveFile(request.ThumbNews);

                    thumb.Type = MediaType.Image;

                    _context.Media.Update(thumb);
                }
            }

            //check topic id
            foreach (var item in request.TopicId)
            {
                var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                if (topic == null)
                {
                    return new ApiErrorResult<bool>($"CannontFindATopicWithId");
                }
            }

            var newsListDelete = await _context.NewsInTopics.Where(n => n.NewsId == request.Id).ToListAsync();

            //delete all news in newsInTopics has request.Id equals NewsId in this table
            foreach (var item in newsListDelete)
            {
                _context.NewsInTopics.Remove(item);

            }

            // update news in topic
            foreach (var item in request.TopicId)
            {
                var newsUpdate = new Data.Entities.NewsInTopics()
                {
                    NewsId = request.Id,
                    TopicId = item
                };

                _context.NewsInTopics.Add(newsUpdate);
            }

            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("UpdateNewsUnsuccessful");
            }

            return new ApiSuccessResult<bool>("UpdateNewsSuccessful", false);
        }

        //Update Link News
        public async Task<ApiResult<bool>> UpdateLink(int newsId, string newLink)
        {
            var news_update = await _context.News.FindAsync(newsId);

            if (news_update == null) 
                   return new ApiErrorResult<bool>($"CannontFindANewsWithId");

            news_update.Content = newLink;

            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("UpdateLinkNewsUnsuccessful");
            }

            return new ApiSuccessResult<bool>("UpdateLinkNewsSuccessful", false);
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