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
using Slugify;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FakeNewsFilter.Application.Catalog;

public interface INewsService
{
    Task<ApiResult<PagedResult<NewsViewModel>>> GetAll(GetManageNewsRequest request);
    Task<ApiResult<PagedResult<NewsViewModel>>> GetBySouce(GetManageNewsRequest request);
    Task<ApiResult<PagedResult<NewsViewModel>>> GetNewsPaging(GetManageNewsRequest request);
    Task<ApiResult<int>> GetViewCount(int NewsId);
    Task<ApiResult<PagedResult<NewsViewModel>>> GetNewsInTopic(GetNewsInTopicRequest request);
    Task<ApiResult<PagedResult<NewsViewModel>>> GetNewsByFollowedTopic(GetNewsFollowedRequest request);
    Task<ApiResult<NewsViewModel>> CreateBySystem(NewsSystemCreateRequest request);
    Task<ApiResult<NewsViewModel>> CreateByOther(NewsOutSourceCreateRequest request);
    Task<ApiResult<string>> Delete(int NewsId);
    Task<ApiResult<NewsViewModel>> GetById(int newsId);
    Task<ApiResult<NewsSystemViewModel>> GetContent(int newsId);
    Task<ApiResult<NewsViewModel>> UpdateBySystem(NewsSystemUpdateRequest request);
    Task<ApiResult<NewsViewModel>> UpdateByOutSource(NewsOutSourceUpdateRequest request);
    Task<ApiResult<NewsViewModel>> Archive(int Id);
    Task<ApiResult<NewsViewModel>> UpdateLink(int newsId, string newLink);
    Task<ApiResult<bool>> UpdateViewCount(Dictionary<int, int> newsViewCountDict);
}

public class NewsService : INewsService
{
    public static readonly List<string> ImageExtensions = new() {".JPG", ".JPE", ".BMP", ".GIF", ".PNG", ".JPEG"};
    private readonly ApplicationDBContext _context;

    private readonly IMapper _mapper;

    private readonly FileStorageService _storageService;

    private SlugHelper _slugHelper;
    private object query;

    public NewsService(ApplicationDBContext context, FileStorageService storageService, IMapper mapper,
        SlugHelper slugHelper)
    {
        _context = context;
        FileStorageService.USER_CONTENT_FOLDER_NAME = "images/news";
        _storageService = storageService;
        _mapper = mapper;
        _slugHelper = slugHelper;
    }

    //Lấy tất cả các tin tức (với Filter là lọc tin giả hay tin thật hoặc tin tạo ra từ hệ thống hoặc nguồn bên ngoài)
    public async Task<ApiResult<PagedResult<NewsViewModel>>> GetAll(GetManageNewsRequest request)
    {
        try
        {
            //1. Lọc dữ liệu
            if (request.LanguageId != null)
            {
                var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);

                if (language == null)
                    return new ApiErrorResult<PagedResult<NewsViewModel>>(404, "LanguageNotFound");
            }

            var newsList = new List<NewsViewModel>();

            LabelNews enum_label;

            if (string.IsNullOrEmpty(request.Keyword))
            {
                //Nếu bộ lọc là null
                newsList = await _context.News
                    .Include(x => x.DetailNews)
                    .Where(n => !string.IsNullOrEmpty(request.LanguageId) ? n.LanguageId == request.LanguageId : true)
                    .Select(x =>
                        new NewsViewModel
                        {
                            NewsId = x.NewsId,
                            Title = x.Title,
                            Content = x.DetailNews != null ? x.DetailNews.Content : "",
                            Description = x.Description,
                            TopicInfo = x.NewsInTopics
                                .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                            OfficialRating = x.OfficialRating.ToString(),
                            SocialBeliefs = x.SocialBeliefs,
                            ViewCount = x.ViewCount,
                            Publisher = x.Publisher,
                            Status = x.Status,
                            SourceCreate = x.SourceCreate.ToString(),
                            ThumbNews = string.IsNullOrEmpty(x.ImageLink)
                                ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia)
                                : x.ImageLink,
                            UrlNews = string.IsNullOrEmpty(x.UrlNews)
                                ? _storageService.GetNewsUrl(x.DetailNews.Alias)
                                : x.UrlNews,
                            LanguageId = x.LanguageId,
                            Timestamp = x.Timestamp
                        }
                    ).ToListAsync();
            }
            else if (Enum.TryParse<LabelNews>(request.Keyword, out enum_label))
            {
                if (string.IsNullOrEmpty(request.LanguageId))
                {
                    //Nếu lọc nhãn tin (thật, giả) mà không quan tâm đến ngôn ngữ tin đó
                    newsList = await _context.News
                        .Where(n => n.OfficialRating.Equals(enum_label))
                        .Select(x => new NewsViewModel
                        {
                            NewsId = x.NewsId,
                            Title = x.Title,
                            Content = x.DetailNews != null ? x.DetailNews.Content : "",
                            Description = x.Description,
                            TopicInfo = x.NewsInTopics
                                .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                            OfficialRating = x.OfficialRating.ToString(),
                            SocialBeliefs = x.SocialBeliefs,
                            ViewCount = x.ViewCount,
                            Publisher = x.Publisher,
                            Status = x.Status,
                            SourceCreate = x.SourceCreate.ToString(),
                            ThumbNews = string.IsNullOrEmpty(x.ImageLink)
                                ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia)
                                : x.ImageLink,
                            UrlNews = string.IsNullOrEmpty(x.UrlNews)
                                ? _storageService.GetNewsUrl(x.DetailNews.Alias)
                                : x.UrlNews,
                            LanguageId = x.LanguageId,
                            Timestamp = x.Timestamp
                        }).ToListAsync();
                }
                else
                {
                    //Nếu bộ lọc không rỗng và languageId không rỗng
                    newsList = await _context.News
                        .Where(n => n.OfficialRating.Equals(enum_label) && n.LanguageId == request.LanguageId)
                        .Select(x => new NewsViewModel
                        {
                            NewsId = x.NewsId,
                            Title = x.Title,
                            Content = x.DetailNews != null ? x.DetailNews.Content : "",
                            Description = x.Description,
                            TopicInfo = x.NewsInTopics
                                .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                            OfficialRating = x.OfficialRating.ToString(),
                            SocialBeliefs = x.SocialBeliefs,
                            ViewCount = x.ViewCount,
                            Publisher = x.Publisher,
                            Status = x.Status,
                            SourceCreate = x.SourceCreate.ToString(),
                            ThumbNews = string.IsNullOrEmpty(x.ImageLink)
                                ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia)
                                : x.ImageLink,
                            UrlNews = string.IsNullOrEmpty(x.UrlNews)
                                ? _storageService.GetNewsUrl(x.DetailNews.Alias)
                                : x.UrlNews,
                            LanguageId = x.LanguageId,
                            Timestamp = x.Timestamp
                        }).ToListAsync();
                }
            }
            else
                return new ApiErrorResult<PagedResult<NewsViewModel>>(404, "OfficalRatingNotFound");

            //2. Phân trang
            int totalRow = newsList.Count();

            //Trường hợp không nhận Pagging thì lấy toàn bộ dữ liệu
            int pageIndex = request.PageIndex == 0 ? 1 : request.PageIndex;

            int pageSize = request.PageSize == 0 ? totalRow : request.PageSize;

            var data = newsList.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new NewsViewModel
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    Content = x.Content ?? "",
                    Description = x.Description,
                    TopicInfo = x.TopicInfo,
                    OfficialRating = x.OfficialRating,
                    SocialBeliefs = x.SocialBeliefs,
                    ViewCount = x.ViewCount,
                    Publisher = x.Publisher,
                    SourceCreate = x.SourceCreate,
                    Status = x.Status,
                    ThumbNews = x.ThumbNews,
                    UrlNews = x.UrlNews,
                    LanguageId = x.LanguageId,
                    Timestamp = x.Timestamp
                }).ToList();

            //4. Hiển thị kết quả
            var pagedResult = new PagedResult<NewsViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = data
            };

            if (pagedResult != null)
                return new ApiSuccessResult<PagedResult<NewsViewModel>>("GetAllNewsSuccessful", pagedResult);
            else
                return new ApiErrorResult<PagedResult<NewsViewModel>>(400, "GetAllNewsUnSuccessful");
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<PagedResult<NewsViewModel>>(500, ex.Message);
        }
    }

    //Lấy các tin tức dựa trên nguồn tạo (từ hệ thống/ nguồn bên ngoài)
    public async Task<ApiResult<PagedResult<NewsViewModel>>> GetBySouce(GetManageNewsRequest request)
    {
        try
        {
            //Keyword = SourceName
            if (string.IsNullOrEmpty(request.Keyword))
            {
                return new ApiErrorResult<PagedResult<NewsViewModel>>(400, "SourceNameNotFound");
            }

            var newsList = new List<NewsViewModel>();

            if (request.Keyword.ToUpper() == "SYSTEM") //Lấy các tin được tạo từ hệ thống
            {
                
                newsList = await _context.News
                    .Include(i => i.DetailNews)
                    .Where(n => n.DetailNews != null)
                    .Select(x => new NewsViewModel
                    {
                        NewsId = x.NewsId,
                        Title = x.Title,
                        Content = x.DetailNews != null ? x.DetailNews.Content : "",
                        Description = x.Description,
                        TopicInfo = x.NewsInTopics
                            .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                        OfficialRating = x.OfficialRating.ToString(),
                        SocialBeliefs = x.SocialBeliefs,
                        ViewCount = x.ViewCount,
                        Publisher = x.Publisher,
                        SourceCreate = x.SourceCreate.ToString(),
                        Status = x.Status,
                        ThumbNews = string.IsNullOrEmpty(x.ImageLink)
                            ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia)
                            : x.ImageLink,
                        UrlNews = string.IsNullOrEmpty(x.UrlNews)
                            ? _storageService.GetNewsUrl(x.DetailNews.Alias)
                            : x.UrlNews,
                        LanguageId = x.LanguageId,
                        Timestamp = x.Timestamp
                    }).ToListAsync();

                //Phân trang
                int totalRow = newsList.Count();

                //Trường hợp không nhận Pagging thì lấy toàn bộ dữ liệu
                int pageIndex = request.PageIndex == 0 ? 1 : request.PageIndex;

                int pageSize = request.PageSize == 0 ? totalRow : request.PageSize;

                var data = newsList.Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize)
                  .Select(x => new NewsViewModel
                  {
                      NewsId = x.NewsId,
                      Title = x.Title,
                      Content = x.Content ?? "",
                      Description = x.Description,
                      TopicInfo = x.TopicInfo,
                      OfficialRating = x.OfficialRating,
                      SocialBeliefs = x.SocialBeliefs,
                      ViewCount = x.ViewCount,
                      Publisher = x.Publisher,
                      SourceCreate = x.SourceCreate,
                      Status = x.Status,
                      ThumbNews = x.ThumbNews,
                      UrlNews = x.UrlNews,
                      LanguageId = x.LanguageId,
                      Timestamp = x.Timestamp
                  }).ToList();

                //Hiển thị kết quả
                var pagedResult = new PagedResult<NewsViewModel>()
                {
                    TotalRecords = totalRow,
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    Items = data
                };


                if (pagedResult != null)
                    return new ApiSuccessResult<PagedResult<NewsViewModel>>("GetAllNewsSuccessful", pagedResult);
                else
                    return new ApiErrorResult<PagedResult<NewsViewModel>>(400, "GetAllNewsUnSuccessful");
            }
            else if (request.Keyword.ToUpper() == "OUTSOURCE") //Lấy các tin được tạo từ nguồn ngoài
            {
                
                newsList = await _context.News
                    .Include(i => i.DetailNews)
                    .Where(n => n.DetailNews == null)
                    .Select(x => new NewsViewModel
                    {
                        NewsId = x.NewsId,
                        Title = x.Title,
                        Content = x.DetailNews != null ? x.DetailNews.Content : "",
                        Description = x.Description,
                        TopicInfo = x.NewsInTopics
                            .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                        OfficialRating = x.OfficialRating.ToString(),
                        SocialBeliefs = x.SocialBeliefs,
                        ViewCount = x.ViewCount,
                        Publisher = x.Publisher,
                        SourceCreate = x.SourceCreate.ToString(),
                        Status = x.Status,
                        ThumbNews = string.IsNullOrEmpty(x.ImageLink)
                            ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia)
                            : x.ImageLink,
                        UrlNews = string.IsNullOrEmpty(x.UrlNews)
                            ? _storageService.GetNewsUrl(x.DetailNews.Alias)
                            : x.UrlNews,
                        LanguageId = x.LanguageId,
                        Timestamp = x.Timestamp
                    }).ToListAsync();

                //Phân trang
                int totalRow = newsList.Count();


                //Trường hợp không nhận Pagging thì lấy toàn bộ dữ liệu
                int pageIndex = request.PageIndex == 0 ? 1 : request.PageIndex;

                int pageSize = request.PageSize == 0 ? totalRow : request.PageSize;

                var data = newsList.Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize)
                  .Select(x => new NewsViewModel
                  {
                      NewsId = x.NewsId,
                      Title = x.Title,
                      Content = x.Content ?? "",
                      Description = x.Description,
                      TopicInfo = x.TopicInfo,
                      OfficialRating = x.OfficialRating,
                      SocialBeliefs = x.SocialBeliefs,
                      ViewCount = x.ViewCount,
                      Publisher = x.Publisher,
                      SourceCreate = x.SourceCreate,
                      Status = x.Status,
                      ThumbNews = x.ThumbNews,
                      UrlNews = x.UrlNews,
                      LanguageId = x.LanguageId,
                      Timestamp = x.Timestamp
                  }).ToList();

                //Hiển thị kết quả
                var pagedResult = new PagedResult<NewsViewModel>()
                {
                    TotalRecords = totalRow,
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    Items = data
                };

                if (pagedResult != null)
                    return new ApiSuccessResult<PagedResult<NewsViewModel>>("GetAllNewsSuccessful", pagedResult);
                else
                    return new ApiErrorResult<PagedResult<NewsViewModel>>(400, "GetAllNewsUnSuccessful");
            }
            else
            {
                return new ApiErrorResult<PagedResult<NewsViewModel>>(400, "GetAllNewsUnSuccessful");
            }
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<PagedResult<NewsViewModel>>(500, ex.Message);
        }
    }

    public async Task<ApiResult<PagedResult<NewsViewModel>>> GetNewsPaging(GetManageNewsRequest request)
    {
        try
        {
            //1. Chạy câu truy vấn 
            if (request.LanguageId != null)
            {
                var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);
                if (language == null)
                {
                    return new ApiErrorResult<PagedResult<NewsViewModel>>(404, "Language Not Found");
                }
            }

            var query = from n in _context.News
                where (string.IsNullOrEmpty(request.LanguageId) || n.LanguageId == request.LanguageId)
                select new
                {
                    NewsId = n.NewsId,
                    Title = n.Title,
                    Content = n.DetailNews != null ? n.DetailNews.Content : "",
                    Description = n.Description,
                    TopicInfo = n.NewsInTopics
                        .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                    OfficialRating = n.OfficialRating.ToString(),
                    SocialBeliefs = n.SocialBeliefs,
                    ViewCount = n.ViewCount,
                    Publisher = n.Publisher,
                    SourceCreate = n.SourceCreate.ToString(),
                    Status = n.Status,
                    ThumbNews = string.IsNullOrEmpty(n.ImageLink)
                        ? _storageService.GetFileUrl(n.DetailNews.Media.PathMedia)
                        : n.ImageLink,
                    UrlNews = string.IsNullOrEmpty(n.UrlNews)
                        ? _storageService.GetNewsUrl(n.DetailNews.Alias)
                        : n.UrlNews,
                    LanguageId = n.LanguageId,
                    Timestamp = n.Timestamp
                };

            //2. Lọc theo điều kiện
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x =>
                    (x.Title.Contains(request.Keyword)) || (x.UrlNews.Contains(request.Keyword)) ||
                    (x.Publisher.Contains(request.Keyword)));
            }


            //3. Phân trang
            int totalRow = await query.CountAsync();

            //Trường hợp không nhận Pagging thì lấy toàn bộ dữ liệu
            int pageIndex = request.PageIndex == 0 ? 1 : request.PageIndex;

            int pageSize = request.PageSize == 0 ? totalRow : request.PageSize;

            var data = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new NewsViewModel
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    Content = x.Content ?? "",
                    Description = x.Description,
                    TopicInfo = x.TopicInfo,
                    OfficialRating = x.OfficialRating,
                    SocialBeliefs = x.SocialBeliefs,
                    ViewCount = x.ViewCount,
                    Publisher = x.Publisher,
                    SourceCreate = x.SourceCreate,
                    Status = x.Status,
                    ThumbNews = x.ThumbNews,
                    UrlNews = x.UrlNews,
                    LanguageId = x.LanguageId,
                    Timestamp = x.Timestamp
                }).ToListAsync();

            //4. Hiển thị kết quả
            var pagedResult = new PagedResult<NewsViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = data
            };

            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<NewsViewModel>>(400, "Get News Unsuccessful", pagedResult);
            }

            return new ApiSuccessResult<PagedResult<NewsViewModel>>("Get News Successful", pagedResult);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<PagedResult<NewsViewModel>>(500, ex.Message);
        }
    }

    //Lấy tổng số lượt xem của một tin tức
    public async Task<ApiResult<int>> GetViewCount(int NewsId)
    {
        try
        {
            var news = await _context.News.FindAsync(NewsId);
            if (news == null)
                return new ApiErrorResult<int>(404, "News Not Found");
            return new ApiSuccessResult<int>("Get Count View Successful", news.ViewCount);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<int>(500, ex.Message);
        }
    }

    //Lấy thông tin 1 tin tức thông qua Id
    public async Task<ApiResult<NewsViewModel>> GetById(int newsId)
    {
        try
        {
            var news = await _context.News
                .Include(t => t.NewsInTopics)
                .Include(t => t.DetailNews)
                .FirstOrDefaultAsync(t => t.NewsId == newsId);


            if (news != null)
            {
                var topic = news.NewsInTopics.Select(o => new TopicInfo
                {
                    TopicId = o.TopicId,
                    TopicName = _context.TopicNews.FirstOrDefault(m => m.TopicId == o.TopicId).Tag
                }).ToList();

                NewsViewModel result = new NewsViewModel
                {
                    NewsId = news.NewsId,
                    Title = news.Title,
                    Content = news.DetailNews != null ? news.DetailNews.Content : "",
                    Description = news.Description,
                    OfficialRating = news.OfficialRating.ToString(),
                    Publisher = news.Publisher,
                    ViewCount = news.ViewCount,
                    SocialBeliefs = news.SocialBeliefs,
                    SourceCreate = news.SourceCreate.ToString(),
                    ThumbNews = string.IsNullOrEmpty(news.ImageLink)
                        ? _storageService.GetFileUrl(_context.Media
                            .FirstOrDefault(x => x.MediaId == news.DetailNews.ThumbNews)?.PathMedia)
                        : news.ImageLink,
                    UrlNews = string.IsNullOrEmpty(news.UrlNews)
                        ? _storageService.GetNewsUrl(news.DetailNews.Alias)
                        : news.UrlNews,
                    LanguageId = news.LanguageId,
                    Timestamp = news.Timestamp,
                    Status = news.Status,
                    TopicInfo = topic.ToList()
                };

                return new ApiSuccessResult<NewsViewModel>("GetThisNewsSuccessful", result);
            }

            return new ApiErrorResult<NewsViewModel>(404, "NewsIsNotFound");
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<NewsViewModel>(500, ex.Message);
        }
    }

    //Lấy nội dung 1 tin tức thông qua Id (chỉ khả dụng với tin được tạo từ hệ thống)
    public async Task<ApiResult<NewsSystemViewModel>> GetContent(int newsId)
    {
        try
        {
            var news = await _context.News
                .Include(t => t.NewsInTopics)
                .Include(t => t.DetailNews)
                .FirstOrDefaultAsync(t => t.NewsId == newsId);

            if (news == null)
            {
                return new ApiErrorResult<NewsSystemViewModel>(404, "NewsIsNotFound");
            }
            else
            {
                //Trường hợp tin từ nguồn ngoài
                if (news.DetailNews == null)
                {
                    return new ApiErrorResult<NewsSystemViewModel>(404, "ContentNewsIsNotFound");
                }

                var topic = news.NewsInTopics.Select(o => new TopicInfo
                {
                    TopicId = o.TopicId,
                    TopicName = _context.TopicNews.FirstOrDefault(m => m.TopicId == o.TopicId).Tag
                }).ToList();

                var checkMedia = _context.Media.FirstOrDefault(x => x.MediaId == news.DetailNews.ThumbNews)?.PathMedia;

                NewsSystemViewModel result = new NewsSystemViewModel
                {
                    NewsId = news.NewsId,
                    Title = news.Title,
                    Description = news.Description,
                    Content = news.DetailNews.Content,
                    OfficialRating = news.OfficialRating.ToString(),
                    Publisher = news.Publisher,
                    SourceCreate = news.SourceCreate.ToString(),
                    ThumbNews = _storageService.GetFileUrl(checkMedia),
                    Alias = news.DetailNews.Alias,
                    LanguageId = news.LanguageId,
                    Timestamp = news.Timestamp,
                    Status = news.Status,
                    TopicInfo = topic.ToList()
                };

                return new ApiSuccessResult<NewsSystemViewModel>("GetThisNewsSuccessful", result);
            }
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<NewsSystemViewModel>(500, ex.Message);
        }
    }

    //Lấy tin tức theo chủ đề mà người dùng theo dõi
    public async Task<ApiResult<PagedResult<NewsViewModel>>> GetNewsByFollowedTopic(GetNewsFollowedRequest request)
    {
        try
        {
            //1. Chạy câu truy vấn 
            var newsVotedByUserId =
                await _context.Vote.Where(x => x.UserId == request.userId).Select(x => x.NewsId).ToListAsync();

            var query = await _context.News
                .Include(i => i.DetailNews)
                .Where(n =>
                    !newsVotedByUserId.Contains(n.NewsId) &&
                    request.topicList.Contains(n.NewsInTopics.FirstOrDefault().TopicId))
                .Select(x => new NewsViewModel
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    Content = x.DetailNews != null ? x.DetailNews.Content : "",
                    Description = x.Description,
                    TopicInfo = x.NewsInTopics.Select(o => new TopicInfo
                            {TopicId = o.TopicId, TopicName = o.TopicNews.Tag})
                        .ToList(),
                    OfficialRating = x.OfficialRating.ToString(),
                    SocialBeliefs = x.SocialBeliefs,
                    ViewCount = x.ViewCount,
                    Publisher = x.Publisher,
                    Status = x.Status,
                    SourceCreate = x.SourceCreate.ToString(),
                    ThumbNews = string.IsNullOrEmpty(x.ImageLink)
                        ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia)
                        : x.ImageLink,
                    UrlNews = string.IsNullOrEmpty(x.UrlNews)
                        ? _storageService.GetNewsUrl(x.DetailNews.Alias)
                        : x.UrlNews,
                    LanguageId = x.LanguageId,
                    Timestamp = x.Timestamp
                }).ToListAsync();

            //2. Phân trang
            int totalRow = query.Count;

            //Trường hợp không nhận Pagging thì lấy toàn bộ dữ liệu
            int pageIndex = request.PageIndex == 0 ? 1 : request.PageIndex;

            int pageSize = request.PageSize == 0 ? totalRow : request.PageSize;


            var data = query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new NewsViewModel
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    Content = x.Content ?? "",
                    Description = x.Description,
                    TopicInfo = x.TopicInfo,
                    OfficialRating = x.OfficialRating,
                    SocialBeliefs = x.SocialBeliefs,
                    ViewCount = x.ViewCount,
                    Publisher = x.Publisher,
                    SourceCreate = x.SourceCreate,
                    Status = x.Status,
                    ThumbNews = x.ThumbNews,
                    UrlNews = x.UrlNews,
                    LanguageId = x.LanguageId,
                    Timestamp = x.Timestamp
                }).ToList();

            //3. Hiển thị kết quả
            var pagedResult = new PagedResult<NewsViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = data
            };

            if (query == null)
                return new ApiErrorResult<PagedResult<NewsViewModel>>(400, "GetNewsByFollowedTopicUnsuccessful");

            return new ApiSuccessResult<PagedResult<NewsViewModel>>("GetNewsByFollowedTopicSuccessful", pagedResult);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<PagedResult<NewsViewModel>>(500, ex.Message);
        }
    }


    //Lấy tất cả các tin tức có trong chủ đề
    public async Task<ApiResult<PagedResult<NewsViewModel>>> GetNewsInTopic(GetNewsInTopicRequest request)
    {
        try
        {
            //1. Truy vấn
            var query = from n in _context.News
                join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                join c in _context.TopicNews on nit.TopicId equals c.TopicId
                select new {n, nit, c};

            query = query.Where(t => request.topicId == t.nit.TopicId);

            //2. Phân trang
            int totalRow = await query.CountAsync();

            //Trường hợp không nhận Pagging thì lấy toàn bộ dữ liệu
            int pageIndex = request.PageIndex == 0 ? 1 : request.PageIndex;

            int pageSize = request.PageSize == 0 ? totalRow : request.PageSize;

            var data = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new NewsViewModel
                {
                    NewsId = x.n.NewsId,
                    Title = x.n.Title,
                    Content = x.n.DetailNews != null ? x.n.DetailNews.Content : "",
                    Description = x.n.Description,
                    OfficialRating = x.n.OfficialRating.ToString(),
                    Publisher = x.n.Publisher,
                    Status = x.n.Status,
                    TopicInfo = x.n.NewsInTopics.Select(o => new TopicInfo
                            {TopicId = o.TopicId, TopicName = o.TopicNews.Tag})
                        .ToList(),
                    SocialBeliefs = x.n.SocialBeliefs,
                    ViewCount = x.n.ViewCount,
                    SourceCreate = x.n.SourceCreate.ToString(),
                    ThumbNews = string.IsNullOrEmpty(x.n.ImageLink)
                        ? _storageService.GetFileUrl(x.n.DetailNews.Media.PathMedia)
                        : x.n.ImageLink,
                    UrlNews = string.IsNullOrEmpty(x.n.UrlNews)
                        ? _storageService.GetNewsUrl(x.n.DetailNews.Alias)
                        : x.n.UrlNews,
                    LanguageId = x.n.LanguageId,
                    Timestamp = x.n.Timestamp
                }).ToListAsync();

            //3. Hiển thị kết quả
            var pagedResult = new PagedResult<NewsViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = data
            };

            if (data == null) return new ApiErrorResult<PagedResult<NewsViewModel>>(400, "GetAllNewsInTopicUnsuccessful");

            if (data.Count == 0)
                return new ApiErrorResult<PagedResult<NewsViewModel>>(404, "DoNotHaveNewsInTopic");

            return new ApiSuccessResult<PagedResult<NewsViewModel>>("GetAllNewsInTopicSuccessful", pagedResult);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<PagedResult<NewsViewModel>>(404, ex.Message);
        }
    }

    //Tạo mới 1 tin tức (từ hệ thống tạo)
    public async Task<ApiResult<NewsViewModel>> CreateBySystem(NewsSystemCreateRequest request)
    {
        using (IDbContextTransaction transaction = _context.Database.BeginTransaction())

            try
            {
                //Kiểm tra ngôn ngữ có tồn tại
                var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);

                if (language == null) return new ApiErrorResult<NewsViewModel>(404, "LanguageNotFound");

                //Kiểm tra chủ đề có tồn tại
                if (request.TopicId == null)
                    return new ApiErrorResult<NewsViewModel>(404, "TopicNotFound");
                foreach (var item in request.TopicId)
                {
                    var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                    if (topic == null)
                        return new ApiErrorResult<NewsViewModel>(404, "TopicNotFound");
                }

                LabelNews label_enum;
                //Kiểm tra nhãn của tin tức, nếu chưa có thì gán undefined 
                if (request.OfficialRating == null)
                {
                    label_enum = LabelNews.undefined;
                }

                //Kiểm tra nhãn của tin tức, nếu không trùng với các nhãn có sẵn thì báo lỗi không tìm thấy nhãn
                if (request.OfficialRating == LabelNews.fake.ToString() ||
                    request.OfficialRating == LabelNews.real.ToString() ||
                    request.OfficialRating == LabelNews.undefined.ToString())
                {
                    if (Enum.TryParse<LabelNews>(request.OfficialRating.ToLower(), out label_enum))
                    {
                        //Bắt đầu tạo tin tức
                        var news = new News
                        {
                            Title = request.Title,
                            Description = request.Description,
                            OfficialRating = label_enum,
                            DatePublished = request.DatePublished ?? DateTime.Now,
                            Publisher = request.Publisher,
                            IsVote = request.isVote,
                            LanguageId = request.LanguageId,
                            Timestamp = DateTime.Now,
                            SourceCreate = SourceCreate.System
                        };
                        _context.News.Add(news);

                        await _context.SaveChangesAsync();

                        //Tạo thông tin chi tiết cho tin tức
                        var detail_news = new DetailNews
                        {
                            NewsId = news.NewsId,
                            Alias = $"{_slugHelper.GenerateSlug(request.Title)}" + "-" + news.NewsId.ToString(),
                            Content = request.Content,
                        };

                        //Lưu hình ảnh trên máy chủ lưu trữ (nếu có)
                        if (request.ThumbNews != null)
                        {
                            var checkExtension =
                                ImageExtensions.Contains(Path.GetExtension(request.ThumbNews.FileName)
                                    .ToUpperInvariant());

                            if (checkExtension == false)
                            {
                                return new ApiErrorResult<NewsViewModel>(400, "FileImageInvalid");
                            }

                            detail_news.Media = new Media
                            {
                                DateCreated = DateTime.Now,
                                FileSize = request.ThumbNews.Length,
                                PathMedia = await SaveFile(request.ThumbNews),
                                Type = checkExtension ? MediaType.Image : MediaType.Video,
                                Caption = "Thumb News " + (checkExtension ? "Image" : "Video")
                            };
                        }

                        _context.DetailNews.Add(detail_news);

                        await _context.SaveChangesAsync();

                        //Gán chủ đề cho tin tức vừa tạo
                        foreach (var topicId in request.TopicId)
                            _context.NewsInTopics.Add(new NewsInTopics
                            {
                                NewsId = news.NewsId,
                                TopicId = topicId,
                                Timestamp = DateTime.Now
                            });

                        var res = await _context.SaveChangesAsync();

                        if (res == 0)
                        {
                            transaction.Rollback();
                             _storageService.DeleteFile(detail_news.Media.PathMedia);
                            return new ApiErrorResult<NewsViewModel>(400, "CreateNewsUnsuccessful");
                        }

                        transaction.Commit();

                        var newsmodel = await GetById(news.NewsId);

                        return new ApiSuccessResult<NewsViewModel>("CreateNewsSuccessful", newsmodel.ResultObj);
                    }
                }

                return new ApiErrorResult<NewsViewModel>(404, "OfficialRatingNotFound");
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return new ApiErrorResult<NewsViewModel>(500, ex.Message);
            }
    }

    //Tạo mới 1 tin tức (từ nguồn bên ngoài
    public async Task<ApiResult<NewsViewModel>> CreateByOther(NewsOutSourceCreateRequest request)
    {
        using (IDbContextTransaction transaction = _context.Database.BeginTransaction())

            try
            {
                //Kiểm tra ngôn ngữ có tồn tại
                var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);

                if (language == null) return new ApiErrorResult<NewsViewModel>(404, "LanguageNotFound");

                //Kiểm tra chủ đề có tồn tại
                if (request.TopicId == null)
                    return new ApiErrorResult<NewsViewModel>(404, "TopicNotFound");
                foreach (var item in request.TopicId)
                {
                    var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                    if (topic == null)
                        return new ApiErrorResult<NewsViewModel>(404, "TopicNotFound");
                }

                LabelNews label_enum;
                SourceCreate source_enum;
                //Kiểm tra nhãn của tin tức, nếu chưa có thì gán undefined 
                if (request.OfficialRating == null)
                {
                    label_enum = LabelNews.undefined;
                }

                //Kiểm tra nhãn của tin tức, nếu không trùng với các nhãn có sẵn thì báo lỗi không tìm thấy nhãn
                if (request.OfficialRating == LabelNews.fake.ToString() ||
                    request.OfficialRating == LabelNews.real.ToString() ||
                    request.OfficialRating == LabelNews.undefined.ToString())
                {
                    if (Enum.TryParse<LabelNews>(request.OfficialRating.ToLower(), out label_enum))
                    {
                        if (Enum.TryParse<SourceCreate>(request.SourceCreate, out source_enum))
                        {
                            //Trường hợp tạo tin tức từ nguồn bên ngoài (Có URL)
                            var news = new News
                            {
                                Title = request.Title,
                                Description = request.Description,
                                UrlNews = request.UrlNews,
                                ImageLink = request.ImageLink,
                                OfficialRating = label_enum,
                                DatePublished = request.DatePublished ?? DateTime.Now,
                                Publisher = request.Publisher,
                                IsVote = request.isVote,
                                LanguageId = request.LanguageId,
                                Timestamp = DateTime.Now,
                                SourceCreate = source_enum
                            };
                            _context.News.Add(news);
                            await _context.SaveChangesAsync();

                            //Gán chủ đề cho tin tức vừa tạo
                            foreach (var topicId in request.TopicId)
                                _context.NewsInTopics.Add(new NewsInTopics
                                {
                                    NewsId = news.NewsId,
                                    TopicId = topicId,
                                    Timestamp = DateTime.Now
                                });

                            var res = await _context.SaveChangesAsync();

                            if (res == 0)
                            {
                                transaction.Rollback();
                                return new ApiErrorResult<NewsViewModel>(400, "CreateNewsUnsuccessful");
                            }

                            transaction.Commit();

                            var newsmodel = await GetById(news.NewsId);

                            return new ApiSuccessResult<NewsViewModel>("CreateNewsSuccessful", newsmodel.ResultObj);
                        }

                        return new ApiErrorResult<NewsViewModel>(404, "SouceCreateNotFound");
                    }
                }


                return new ApiErrorResult<NewsViewModel>(404, "OfficalRatingNotFound");
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return new ApiErrorResult<NewsViewModel>(500, ex.Message);
            }
    }

    //Xoá tin tức
    public async Task<ApiResult<string>> Delete(int newsId)
    {
        //Kiểm tra tin có tồn tại không trước khi xoá
        var news = await NewsCommon.CheckExistNews(_context, newsId);

        if (news == null)
        {
            return new ApiErrorResult<string>(404, "CannontFindANewsWithId", " " + newsId.ToString());
        }

        if (news.DetailNews != null)
        {
            //Xoá ảnh trước
            if (news.DetailNews.ThumbNews != null)
            {
                var media = _context.Media.Single(x => x.MediaId == news.DetailNews.ThumbNews);

                if (media != null && media.PathMedia != null)
                {
                     _storageService.DeleteFile(media.PathMedia);
                    _context.Media.Remove(media);
                }
            }

            //Xoá chi tiết tin
            _context.DetailNews.Remove(news.DetailNews);
        }

        //Xoá tin
        _context.News.Remove(news);

        var result = await _context.SaveChangesAsync();

        if (result == 0) return new ApiErrorResult<string>(400, "DeleteNewsUnsuccessful");

        return new ApiSuccessResult<string>("DeleteNewsSuccessful", " " + news.NewsId.ToString());
    }


    //Cập nhật tin tức (trừ Vote) bởi các nguồn ngoài
    public async Task<ApiResult<NewsViewModel>> UpdateByOutSource(NewsOutSourceUpdateRequest request)
    {
        using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
        {
            try
            {
                //Kiểm tra tin tức
                var news_update = await NewsCommon.CheckExistNews(_context, request.Id);

                if (news_update == null)
                    return new ApiErrorResult<NewsViewModel>(404, "CannontFindANewsWithId");

                //Trường hợp tin lấy từ nguồn ngoài (Không phải từ hệ thống tạo)
                if (news_update.DetailNews != null)
                {
                    return new ApiErrorResult<NewsViewModel>(400, "NewsCreateBySystem");
                }
                else
                {
                    news_update.Title = request.Title ?? news_update.Title;
                    news_update.Description = request.Description ?? news_update.Description;
                    news_update.LanguageId = request.LanguageId ?? news_update.LanguageId;
                    news_update.ImageLink = request.ImageLink ?? news_update.ImageLink;
                    news_update.UrlNews = request.UrlNews ?? news_update.UrlNews;
                    news_update.Publisher = request.Publisher ?? news_update.Publisher;
                    news_update.LanguageId = request.LanguageId ?? news_update.LanguageId;
                    news_update.Timestamp = DateTime.Now;

                    if (request.OfficialRating != null)
                    {
                        LabelNews label_enum;

                        if (Enum.TryParse<LabelNews>(request.OfficialRating.ToLower(), out label_enum))
                        {
                            news_update.OfficialRating = label_enum;
                        }
                        else
                        {
                            return new ApiErrorResult<NewsViewModel>(404, "OfficalRatingNotFound");
                        }
                    }

                    if (request.TopicId == null)
                    {
                        return new ApiErrorResult<NewsViewModel>(404, "TopicNotEmpty");
                    }
                    //Tìm các Topic của News đó

                    var newsListCanDelete = news_update.NewsInTopics.Select(x => x.TopicId).ToList();

                    //So sánh có sự thay đổi chủ đề của tin tức đó không?
                    var compareTopic = Enumerable.SequenceEqual(request.TopicId, newsListCanDelete);

                    if (compareTopic == false)
                    {
                        //Kiểm tra id chủ đề
                        foreach (var item in request.TopicId)
                        {
                            var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                            if (topic == null)
                                return new ApiErrorResult<NewsViewModel>(404, "CannontFindATopicWithId");
                        }

                        //Xóa tất cả tin tức trong newsInTopics đã yêu cầu. 
                        foreach (var item in news_update.NewsInTopics)
                            _context.NewsInTopics.Remove(item);

                        //Cập nhật tin tức trong chủ đề
                        foreach (var item in request.TopicId)
                        {
                            var newsUpdate = new NewsInTopics
                            {
                                NewsId = request.Id,
                                TopicId = item,
                                Timestamp = DateTime.Now
                            };

                            _context.NewsInTopics.Add(newsUpdate);
                        }
                    }

                    _context.News.Update(news_update);

                    var res = await _context.SaveChangesAsync();
                    if (res == 0)
                    {
                        transaction.Rollback();
                        return new ApiErrorResult<NewsViewModel>(400, "UpdateNewsUnsuccessful");
                    }

                    transaction.Commit();

                    var newsmodel = await GetById(news_update.NewsId);

                    return new ApiSuccessResult<NewsViewModel>("UpdateNewsSuccessful", newsmodel.ResultObj);
                }
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return new ApiErrorResult<NewsViewModel>(500, ex.Message);
            }
        }
    }

    //Cập nhật tin tức (trừ Vote) bởi hệ thống
    public async Task<ApiResult<NewsViewModel>> UpdateBySystem(NewsSystemUpdateRequest request)
    {
        using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
        {
            try
            {
                //Kiểm tra tin tức
                var news_update = await NewsCommon.CheckExistNews(_context, request.Id);

                if (news_update == null)
                    return new ApiErrorResult<NewsViewModel>(404, "CannontFindANewsWithId");

                //Trường hợp tin tức không được tạo từ hệ thống
                if (news_update.DetailNews == null)
                {
                    return new ApiErrorResult<NewsViewModel>(400, "NewsCreateBySourceOther");
                }

                var dnews = news_update.DetailNews;

                dnews.Content = request.Content ?? dnews.Content;

                if (request.ThumbNews != null)
                {
                    //Kiểm tra hình đã có trên DB chưa
                    var thumb = _context.Media.FirstOrDefault(i => i.MediaId == dnews.ThumbNews);

                    //Nếu chưa có hình thì thêm hình mới
                    if (thumb == null)
                    {
                        //Kiểm tra định dạng file đưa vào
                        var checkExtension =
                            ImageExtensions.Contains(Path.GetExtension(request.ThumbNews.FileName)
                                .ToUpperInvariant());

                        if (checkExtension == false)
                        {
                            return new ApiErrorResult<NewsViewModel>(400, "FileImageInvalid");
                        }

                        dnews.Media = new Media
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
                        if (thumb.PathMedia != null)  _storageService.DeleteFile(thumb.PathMedia);
                        thumb.FileSize = request.ThumbNews.Length;
                        thumb.PathMedia = await SaveFile(request.ThumbNews);

                        thumb.Type = MediaType.Image;

                        _context.Media.Update(thumb);
                    }
                }

                //Cập nhật chi tiết tin tức trước
                _context.DetailNews.Update(dnews);

                //Cập nhật các thông tin chính sau
                news_update.Title = request.Title ?? news_update.Title;
                news_update.Description = request.Description ?? news_update.Description;
                news_update.LanguageId = request.LanguageId ?? news_update.LanguageId;
                news_update.Publisher = request.Publisher ?? news_update.Publisher;
                news_update.LanguageId = request.LanguageId ?? news_update.LanguageId;
                news_update.Timestamp = DateTime.Now;

                if (request.OfficialRating != null)
                {
                    LabelNews label_enum;

                    if (Enum.TryParse<LabelNews>(request.OfficialRating.ToLower(), out label_enum))
                    {
                        news_update.OfficialRating = label_enum;
                    }
                    else
                    {
                        return new ApiErrorResult<NewsViewModel>(404, "OfficalRatingNotFound");
                    }
                }

                if (request.TopicId == null)
                {
                    return new ApiErrorResult<NewsViewModel>(404, "TopicNotEmpty");
                }

                //Tìm các Topic của News đó
                var newsListCanDelete = news_update.NewsInTopics.Select(x => x.TopicId).ToList();

                //So sánh có sự thay đổi chủ đề của tin tức đó không?
                var compareTopic = Enumerable.SequenceEqual(request.TopicId, newsListCanDelete);

                if (compareTopic == false)
                {
                    //Kiểm tra id chủ đề
                    foreach (var item in request.TopicId)
                    {
                        var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                        if (topic == null)
                            return new ApiErrorResult<NewsViewModel>(404, "CannontFindATopic");
                    }

                    //Xóa tất cả tin tức trong newsInTopics đã yêu cầu. 
                    foreach (var item in news_update.NewsInTopics)
                        _context.NewsInTopics.Remove(item);

                    //Cập nhật tin tức trong chủ đề
                    foreach (var item in request.TopicId)
                    {
                        var newsUpdate = new NewsInTopics
                        {
                            NewsId = request.Id,
                            TopicId = item,
                            Timestamp = DateTime.Now
                        };

                        _context.NewsInTopics.Add(newsUpdate);
                    }
                }

                var res = await _context.SaveChangesAsync();
                if (res == 0)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<NewsViewModel>(400, "UpdateNewsUnsuccessful");
                }

                transaction.Commit();

                var newsmodel = await GetById(news_update.NewsId);

                return new ApiSuccessResult<NewsViewModel>("UpdateNewsSuccessful", newsmodel.ResultObj);
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return new ApiErrorResult<NewsViewModel>(500, ex.Message);
            }
        }
    }


    //Cập nhật đường dẫn tin tức
    public async Task<ApiResult<NewsViewModel>> UpdateLink(int newsId, string newLink)
    {
        try
        {
            var news_update = await NewsCommon.CheckExistNews(_context, newsId);

            if (news_update == null)
                return new ApiErrorResult<NewsViewModel>(404, "CannontFindANews");

            news_update.UrlNews = newLink;

            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<NewsViewModel>(400, "UpdateLinkNewsUnsuccessful");

            var newsmodel = await GetById(news_update.NewsId);

            return new ApiSuccessResult<NewsViewModel>("UpdateLinkNewsSuccessful", newsmodel.ResultObj);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<NewsViewModel>(500, ex.Message);
        }
    }

    //Lưu file ảnh
    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
         _storageService.SaveFile(file.OpenReadStream(), fileName);
        return fileName;
    }

    public async Task<ApiResult<NewsViewModel>> Archive(int Id)
    {
        try
        {
            var news = await NewsCommon.CheckExistNews(_context, Id);

            if (news == null)
                return new ApiErrorResult<NewsViewModel>(404, "CannontFindComment");

            news.Status = Status.Archive;

            var result = await _context.SaveChangesAsync();
            if (result == 0) return new ApiErrorResult<NewsViewModel>(400, "UpdateLinkNewsUnsuccessful");

            var newsmodel = await GetById(news.NewsId);

            return new ApiSuccessResult<NewsViewModel>("UpdateLinkNewsSuccessful", newsmodel.ResultObj);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<NewsViewModel>(500, ex.Message);
        }
    }

    public async Task<ApiResult<bool>> UpdateViewCount(Dictionary<int, int> newsViewCountDict)
    {
        try
        {
            foreach (var item in newsViewCountDict)
            {
                var news = await NewsCommon.CheckExistNews(_context, item.Key);

                if (news == null)
                    return new ApiErrorResult<bool>(404, "News not found");

                news.ViewCount = item.Value;
                _context.News.Update(news);
            }

            var result = await _context.SaveChangesAsync();
            if (result == 0)
                return new ApiErrorResult<bool>(400, "Update View Count News Unsuccessful");
            return new ApiSuccessResult<bool>("Update View Count News Successful");
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(500, ex.Message);
        }
    }
}