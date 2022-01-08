using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FakeNewsFilter.Application.Catalog;

public interface INewsService
{
    Task<ApiResult<List<NewsViewModel>>> GetAll(string languageId, string filter);

    Task<ApiResult<List<NewsViewModel>>> GetNewsInTopic(int topicId);

    Task<ApiResult<List<NewsViewModel>>> GetNewsByFollowedTopic(List<int> topicList, Guid userId);

    Task<ApiResult<string>> Create(NewsCreateRequest request);

    Task<ApiResult<string>> Delete(int NewsId);

    Task<ApiResult<NewsViewModel>> GetById(int newsId);

    Task<ApiResult<string>> Update(NewsUpdateRequest request);

    Task<ApiResult<string>> UpdateLink(int newsId, string newLink);
}

public class NewsService : INewsService
{
    public static readonly List<string> ImageExtensions = new() {".JPG", ".JPE", ".BMP", ".GIF", ".PNG"};
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

    //Lấy tất cả các tin tức
    public async Task<ApiResult<List<NewsViewModel>>> GetAll(string languageId, string filter)
    {
        var language = await _context.Languages.SingleOrDefaultAsync(x => x.Id == languageId);

        var newsList = new List<NewsViewModel>();

        if (string.IsNullOrEmpty(filter))

            // nếu bộ lọc là null
            newsList = await _context.News
                .Where(n => !string.IsNullOrEmpty(languageId) ? n.LanguageId == languageId : true)
                .Select(x =>
                    new NewsViewModel
                    {
                        NewsId = x.NewsId,
                        Name = x.Name,
                        TopicInfo = x.NewsInTopics
                            .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                        Description = x.Description,
                        Content = x.Content,
                        OfficialRating = x.OfficialRating,
                        Publisher = x.Publisher,
                        Status = x.Status,
                        ThumbNews = x.Media.PathMedia,
                        LanguageId = x.LanguageId,
                        Timestamp = x.Timestamp
                    }
                ).ToListAsync();
        else if (string.IsNullOrEmpty(languageId))

            // nếu bộ lọc không null và languageId là null
            newsList = await _context.News.Where(n => n.OfficialRating.ToLower().Contains(filter.ToLower()))
                .Select(x => new NewsViewModel
                {
                    NewsId = x.NewsId,
                    Name = x.Name,
                    TopicInfo = x.NewsInTopics
                        .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                    Description = x.Description,
                    Content = x.Content,
                    OfficialRating = x.OfficialRating,
                    Publisher = x.Publisher,
                    Status = x.Status,
                    ThumbNews = x.Media.PathMedia,
                    LanguageId = x.LanguageId,
                    Timestamp = x.Timestamp
                }).ToListAsync();
        else

            //nếu bộ lọc không rỗng và languageId không rỗng
                        newsList = await _context.News.Where(n =>
                    n.OfficialRating.ToLower().Contains(filter.ToLower()) && n.LanguageId == languageId)
                .Select(x => new NewsViewModel
                {
                    NewsId = x.NewsId,
                    Name = x.Name,
                    TopicInfo = x.NewsInTopics
                        .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                    Description = x.Description,
                    Content = x.Content,
                    OfficialRating = x.OfficialRating,
                    Publisher = x.Publisher,
                    Status = x.Status,
                    ThumbNews = x.Media.PathMedia,
                    LanguageId = x.LanguageId,
                    Timestamp = x.Timestamp
                }).ToListAsync();

        if (language == null) return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsSuccessful", newsList);

        return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsSuccessful", newsList);
    }

    //Lấy thông tin News thông qua Id
    public async Task<ApiResult<NewsViewModel>> GetById(int newsId)
    {
        var news = await _context.News.Include(t => t.NewsInTopics).FirstOrDefaultAsync(t => t.NewsId == newsId);

        NewsViewModel result = null;

        if (news != null)
        {
            var topic = news.NewsInTopics.Select(o => new TopicInfo
                {
                    TopicId = o.TopicId, TopicName = _context.TopicNews.FirstOrDefault(m => m.TopicId == o.TopicId).Tag
                })
                .ToList();

            var media = _context.Media.Where(x => x.MediaId == news.ThumbNews).FirstOrDefault();

            result = new NewsViewModel
            {
                NewsId = news.NewsId,
                Name = news.Name,
                Description = news.Description,
                Content = news.Content,
                OfficialRating = news.OfficialRating,
                Publisher = news.Publisher,
                ThumbNews = string.IsNullOrEmpty(news.Media?.PathMedia) ? null : media.PathMedia,
                LanguageId = news.LanguageId,
                Timestamp = news.Timestamp,
                Status = news.Status,
                TopicInfo = topic.ToList()
            };

            return new ApiSuccessResult<NewsViewModel>("GetThisNewsSuccessful", result);
        }

        return new ApiErrorResult<NewsViewModel>("NewsIsNotFound");
    }

    //Lấy tin tức theo chủ đề mà người dùng theo dõi
    public async Task<ApiResult<List<NewsViewModel>>> GetNewsByFollowedTopic(List<int> topicList, Guid userId)
    {
        var newsVotedByUserId = await _context.Vote.Where(x => x.UserId == userId).Select(x => x.NewsId).ToListAsync();

        var newsList = await _context.News.Where(n =>
                !newsVotedByUserId.Contains(n.NewsId) && topicList.Contains(n.NewsInTopics.FirstOrDefault().TopicId))
            .Select(x => new NewsViewModel
            {
                NewsId = x.NewsId,
                Name = x.Name,
                TopicInfo = x.NewsInTopics.Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag})
                    .ToList(),
                Description = x.Description,
                Content = x.Content,
                OfficialRating = x.OfficialRating,
                Publisher = x.Publisher,
                Status = x.Status,
                ThumbNews = x.Media.PathMedia,
                LanguageId = x.LanguageId,
                Timestamp = x.Timestamp
            }).ToListAsync();


        if (newsList == null) return new ApiErrorResult<List<NewsViewModel>>("GetNewsByFollowedTopicUnsuccessful");

        return new ApiSuccessResult<List<NewsViewModel>>("GetNewsByFollowedTopicSuccessful", newsList);
    }


    //Lấy tất cả các tin tức có trong chủ đề
    public async Task<ApiResult<List<NewsViewModel>>> GetNewsInTopic(int topicId)
    {
        var query = from n in _context.News
            join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
            join c in _context.TopicNews on nit.TopicId equals c.TopicId
            select new {n, nit, c};

        query = query.Where(t => topicId == t.nit.TopicId);

        var data = await query
            .Select(x => new NewsViewModel
            {
                NewsId = x.n.NewsId,
                Name = x.n.Name,
                LanguageId = x.n.LanguageId,
                Description = x.n.Description,
                Content = x.n.Content,
                Status = x.n.Status,
                ThumbNews = x.n.Media.PathMedia,
                Timestamp = x.n.Timestamp
            }).ToListAsync();

        if (data == null) return new ApiErrorResult<List<NewsViewModel>>("GetAllNewsInTopicUnsuccessful");
        if (data.Count == 0) return new ApiSuccessResult<List<NewsViewModel>>("DoNotHaveNewsInTopic");
        return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsInTopicSuccessful", data);
    }

    //Tạo mới 1 tin tức
    public async Task<ApiResult<string>> Create(NewsCreateRequest request)
    {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            
                try
                {
                    //kiểm tra ngôn ngữ
                    var language = await _context.Languages.FirstOrDefaultAsync(x => x.Id == request.LanguageId);
                    if (language == null) return new ApiErrorResult<string>("LanguageNotFound", " " + request.LanguageId);

                    //Kiểm tra id chủ đề
                    foreach (var item in request.TopicId)
                    {
                        var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                        if (topic == null) return new ApiErrorResult<string>("TopicNotFound", " " + request.TopicId);
                    }

                    var news = new News
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

                    //Lưu hình ảnh trên máy chủ lưu trữ
                    if (request.ThumbNews != null)
                        {
                            var checkExtension =
                                ImageExtensions.Contains(Path.GetExtension(request.ThumbNews.FileName).ToUpperInvariant());

                            if (checkExtension == false)
                            {
                                return new ApiErrorResult<string>("FileImageInvalid", " " + checkExtension);
                            }
                        news.Media = new Media
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

                        foreach (var topicId in request.TopicId)
                            _context.NewsInTopics.Add(new NewsInTopics
                            {
                                NewsId = news.NewsId,
                                TopicId = topicId
                            });

                        var result = await _context.SaveChangesAsync();
                        if (result == 0)
                        {
                            transaction.Rollback();
                            await _storageService.DeleteFileAsync(news.Media.PathMedia);
                            return new ApiErrorResult<string>("CreateNewsUnsuccessful", result);
                        }

                        transaction.Commit();
                        return new ApiSuccessResult<string>("CreateNewsSuccessful", news.NewsId.ToString());
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<string>(ex.Message);
                }
            
    }

    //Xoá tin tức
    public async Task<ApiResult<string>> Delete(int newsId)
    {
        var news = await _context.News.FindAsync(newsId);

        if (news == null)
            return new ApiErrorResult<string>("CannontFindANewsWithId", newsId);

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
        var result = await _context.SaveChangesAsync();
        if (result == 0) return new ApiErrorResult<string>("DeleteNewsUnsuccessful"," " + result);

        return new ApiSuccessResult<string>("DeleteNewsSuccessful", " " + news.NewsId.ToString());
    }


    //Cập nhật tin tức
    public async Task<ApiResult<string>> Update(NewsUpdateRequest request)
    {
        using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
        {
            try
            {
                var news_update = await _context.News.FindAsync(request.Id);

                if (news_update == null)
                    return new ApiErrorResult<string>("CannontFindANewsWithId", request.Id);

                news_update.Name = request.Name ?? news_update.Name;
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
                        //Kiểm tra định dạng file đưa vào
                        var checkExtension =
                            ImageExtensions.Contains(Path.GetExtension(request.ThumbNews.FileName).ToUpperInvariant());

                        if (checkExtension == false)
                        {
                            return new ApiErrorResult<string>("FileImageInvalid", checkExtension.ToString());
                        }

                        news_update.Media = new Media
                        {
                            Caption = "Thumbnail Topic",
                            DateCreated = DateTime.Now,
                            FileSize = request.ThumbNews.Length,
                            PathMedia = await SaveFile(request.ThumbNews),
                            Type = MediaType.Image,
                            SortOrder = 1
                        };
                    }
                    else
                    {
                        if (thumb.PathMedia != null) await _storageService.DeleteFileAsync(thumb.PathMedia);
                        thumb.FileSize = request.ThumbNews.Length;
                        thumb.PathMedia = await SaveFile(request.ThumbNews);

                        thumb.Type = MediaType.Image;

                        _context.Media.Update(thumb);
                    }
                }

                //Kiểm tra id chủ đề
                foreach (var item in request.TopicId)
                {
                    var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                    if (topic == null) return new ApiErrorResult<string>("CannontFindATopicWithId", " " + request.TopicId);
                }

                var newsListDelete = await _context.NewsInTopics.Where(n => n.NewsId == request.Id).ToListAsync();

                //xóa tất cả tin tức trong newsInTopics đã yêu cầu. 
                foreach (var item in newsListDelete) _context.NewsInTopics.Remove(item);

                // Cập nhật tin tức trong chủ đề
                foreach (var item in request.TopicId)
                {
                    var newsUpdate = new NewsInTopics
                    {
                        NewsId = request.Id,
                        TopicId = item
                    };

                    
                    _context.NewsInTopics.Add(newsUpdate);
                }

                var result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<string>("UpdateNewsUnsuccessful"," " + result);
                } 

                transaction.Commit();
                return new ApiSuccessResult<string>("UpdateNewsSuccessful", " " + news_update.NewsId.ToString());
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return new ApiErrorResult<string>(ex.Message);
            }

        }

        
    }

    //Cập nhật đường dẫn tin tức
    public async Task<ApiResult<string>> UpdateLink(int newsId, string newLink)
    {
        var news_update = await _context.News.FindAsync(newsId);

        if (news_update == null)
            return new ApiErrorResult<string>("CannontFindANewsWithId", newsId);

        news_update.Content = newLink;

        var result = await _context.SaveChangesAsync();
        if (result == 0) return new ApiErrorResult<string>("UpdateLinkNewsUnsuccessful", result);

        return new ApiSuccessResult<string>("UpdateLinkNewsSuccessful", newLink);
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        return fileName;
    }
}