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

namespace FakeNewsFilter.Application.Catalog;

public interface INewsService
{
    Task<ApiResult<List<NewsViewModel>>> GetAll(string languageId, string filter);
    Task<ApiResult<List<NewsViewModel>>> GetBySouce(string source_name);
    Task<ApiResult<int>> GetViewCount(int NewsId);
    Task<ApiResult<List<NewsViewModel>>> GetNewsInTopic(int topicId);
    Task<ApiResult<List<NewsViewModel>>> GetNewsByFollowedTopic(List<int> topicList, Guid userId);
    Task<ApiResult<string>> CreateBySystem(NewsSystemCreateRequest request);
    Task<ApiResult<string>> CreateByOther(NewsOutSourceCreateRequest request);
    Task<ApiResult<string>> Delete(int NewsId);
    Task<ApiResult<NewsViewModel>> GetById(int newsId);
    Task<ApiResult<NewsSystemViewModel>> GetContent(int newsId);
    Task<ApiResult<string>> UpdateBySystem(NewsSystemUpdateRequest request);
    Task<ApiResult<string>> UpdateByOutSource(NewsOutSourceUpdateRequest request);
    Task<ApiResult<string>> Archive(int Id);
    Task<ApiResult<string>> UpdateLink(int newsId, string newLink);
    Task<ApiResult<bool>> UpdateViewCount(Dictionary<int, int> newsViewCountDict);
}

public class NewsService : INewsService
{
    public static readonly List<string> ImageExtensions = new() {".JPG", ".JPE", ".BMP", ".GIF", ".PNG", ".JPEG"};
    private readonly ApplicationDBContext _context;

    private readonly IMapper _mapper;

    private readonly FileStorageService _storageService;

    private SlugHelper _slugHelper;

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
    public async Task<ApiResult<List<NewsViewModel>>> GetAll(string languageId, string label_news_filter)
    {
        try
        {
            if(languageId !=null)
            {
                var language = await LanguageCommon.CheckExistLanguage(_context, languageId);

                if (language == null)
                    return new ApiErrorResult<List<NewsViewModel>>(404, "LanguageNotFound");

            }

            var newsList = new List<NewsViewModel>();

            LabelNews enum_label;

            if (string.IsNullOrEmpty(label_news_filter))
            {
                //Nếu bộ lọc là null
                newsList = await _context.News
                .Include(x => x.DetailNews)
                .Where(n => !string.IsNullOrEmpty(languageId) ? n.LanguageId == languageId : true)
                .Select(x =>
                    new NewsViewModel
                    {
                        NewsId = x.NewsId,
                        Title = x.Title,
                        TopicInfo = x.NewsInTopics
                            .Select(o => new TopicInfo { TopicId = o.TopicId, TopicName = o.TopicNews.Tag }).ToList(),
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
            else if (Enum.TryParse<LabelNews>(label_news_filter, out enum_label))
            {
                if (string.IsNullOrEmpty(languageId))
                {
                    //Nếu lọc nhãn tin (thật, giả) mà không quan tâm đến ngôn ngữ tin đó
                    newsList = await _context.News
                        .Where(n => n.OfficialRating.Equals(enum_label))
                        .Select(x => new NewsViewModel
                        {
                            NewsId = x.NewsId,
                            Title = x.Title,
                            TopicInfo = x.NewsInTopics
                                .Select(o => new TopicInfo { TopicId = o.TopicId, TopicName = o.TopicNews.Tag }).ToList(),
                            OfficialRating = x.OfficialRating.ToString(),
                            SocialBeliefs = x.SocialBeliefs,
                            ViewCount = x.ViewCount,
                            Publisher = x.Publisher,
                            Status = x.Status,
                            SourceCreate = x.SourceCreate.ToString(),
                            ThumbNews = string.IsNullOrEmpty(x.ImageLink)
                                ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia)
                                : x.ImageLink,
                            UrlNews = string.IsNullOrEmpty(x.UrlNews) ? _storageService.GetNewsUrl(x.DetailNews.Alias) : x.UrlNews,
                            LanguageId = x.LanguageId,
                            Timestamp = x.Timestamp
                        }).ToListAsync();
                }
                else
                {
                    //Nếu bộ lọc không rỗng và languageId không rỗng
                    newsList = await _context.News
                        .Where(n => n.OfficialRating.Equals(enum_label) && n.LanguageId == languageId)
                        .Select(x => new NewsViewModel
                        {
                            NewsId = x.NewsId,
                            Title = x.Title,
                            TopicInfo = x.NewsInTopics
                                .Select(o => new TopicInfo { TopicId = o.TopicId, TopicName = o.TopicNews.Tag }).ToList(),
                            OfficialRating = x.OfficialRating.ToString(),
                            SocialBeliefs = x.SocialBeliefs,
                            ViewCount = x.ViewCount,
                            Publisher = x.Publisher,
                            Status = x.Status,
                            SourceCreate = x.SourceCreate.ToString(),
                            ThumbNews = string.IsNullOrEmpty(x.ImageLink)
                                ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia)
                                : x.ImageLink,
                            UrlNews = string.IsNullOrEmpty(x.UrlNews) ? _storageService.GetNewsUrl(x.DetailNews.Alias) : x.UrlNews,
                            LanguageId = x.LanguageId,
                            Timestamp = x.Timestamp
                        }).ToListAsync();
                }
            }
            else
                return new ApiErrorResult<List<NewsViewModel>>(404, "OfficalRatingNotFound");

            if (newsList.Count > 0)
                return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsSuccessful", newsList);
            else
                return new ApiErrorResult<List<NewsViewModel>>(400, "GetAllNewsUnSuccessful");
        }
        catch(Exception ex)
        {
            return new ApiErrorResult<List<NewsViewModel>>(500, ex.Message);
        }
    }

    //Lấy các tin tức dựa trên nguồn tạo (từ hệ thống/ nguồn bên ngoài)
    public async Task<ApiResult<List<NewsViewModel>>> GetBySouce(string source_name)
    {
        try
        {
            if(string.IsNullOrEmpty(source_name))
            {
                return new ApiErrorResult<List<NewsViewModel>>(400, "SourceNameNotFound");
            }

            var newsList = new List<NewsViewModel>();

            if (source_name.ToUpper() == "SYSTEM") //Lấy các tin được tạo từ hệ thống
            {

                newsList = await _context.News
                   .Include(i => i.DetailNews)
                   .Where(n => n.DetailNews != null)
                   .Select(x => new NewsViewModel
                   {
                       NewsId = x.NewsId,
                       Title = x.Title,
                       TopicInfo = x.NewsInTopics
                           .Select(o => new TopicInfo { TopicId = o.TopicId, TopicName = o.TopicNews.Tag }).ToList(),
                       OfficialRating = x.OfficialRating.ToString(),
                       SocialBeliefs = x.SocialBeliefs,
                       ViewCount = x.ViewCount,
                       Publisher = x.Publisher,
                       SourceCreate = x.SourceCreate.ToString(),
                       Status = x.Status,
                       ThumbNews = string.IsNullOrEmpty(x.ImageLink)
                           ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia)
                           : x.ImageLink,
                       UrlNews = string.IsNullOrEmpty(x.UrlNews) ? _storageService.GetNewsUrl(x.DetailNews.Alias) : x.UrlNews,
                       LanguageId = x.LanguageId,
                       Timestamp = x.Timestamp
                   }).ToListAsync();

                if (newsList.Count > 0)
                    return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsSuccessful", newsList);
                else
                    return new ApiErrorResult<List<NewsViewModel>>(400, "GetAllNewsUnSuccessful");
            }
            else if (source_name.ToUpper() == "OUTSOURCE") //Lấy các tin được tạo từ nguồn ngoài
            {

                newsList = await _context.News
                   .Include(i => i.DetailNews)
                   .Where(n => n.DetailNews == null)
                   .Select(x => new NewsViewModel
                   {
                       NewsId = x.NewsId,
                       Title = x.Title,
                       TopicInfo = x.NewsInTopics
                           .Select(o => new TopicInfo { TopicId = o.TopicId, TopicName = o.TopicNews.Tag }).ToList(),
                       OfficialRating = x.OfficialRating.ToString(),
                       SocialBeliefs = x.SocialBeliefs,
                       ViewCount = x.ViewCount,
                       Publisher = x.Publisher,
                       SourceCreate = x.SourceCreate.ToString(),
                       Status = x.Status,
                       ThumbNews = string.IsNullOrEmpty(x.ImageLink)
                           ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia)
                           : x.ImageLink,
                       UrlNews = string.IsNullOrEmpty(x.UrlNews) ? _storageService.GetNewsUrl(x.DetailNews.Alias) : x.UrlNews,
                       LanguageId = x.LanguageId,
                       Timestamp = x.Timestamp
                   }).ToListAsync();

                if (newsList.Count > 0)
                    return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsSuccessful", newsList);
                else
                    return new ApiErrorResult<List<NewsViewModel>>(400, "GetAllNewsUnSuccessful");
            }
            else
            {
                return new ApiErrorResult<List<NewsViewModel>>(400, "GetAllNewsUnSuccessful");
            }


        }
        catch(Exception ex)
        {
            return new ApiErrorResult<List<NewsViewModel>>(500, ex.Message);
        }
    }

        //Lấy tổng số lượt xem của một tin tức
        public async Task<ApiResult<int>> GetViewCount(int NewsId)
    {
        try
        {
            var news = await _context.News.FindAsync(NewsId);
            if (news == null)
                return new ApiErrorResult<int>(404, "NewsNotFound");
            return new ApiSuccessResult<int>("GetCountViewSuccessful", news.ViewCount);
        }
        catch(Exception ex)
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
        catch(Exception ex)
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

            if(news == null)
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
        catch(Exception ex)
        {
            return new ApiErrorResult<NewsSystemViewModel>(500, ex.Message);

        }
    }

    //Lấy tin tức theo chủ đề mà người dùng theo dõi
    public async Task<ApiResult<List<NewsViewModel>>> GetNewsByFollowedTopic(List<int> topicList, Guid userId)
    {
        try
        {
            var newsVotedByUserId = await _context.Vote.Where(x => x.UserId == userId).Select(x => x.NewsId).ToListAsync();

            var newsList = await _context.News
                .Include(i => i.DetailNews)
                .Where(n =>
                    !newsVotedByUserId.Contains(n.NewsId) && topicList.Contains(n.NewsInTopics.FirstOrDefault().TopicId))
                .Select(x => new NewsViewModel
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    TopicInfo = x.NewsInTopics.Select(o => new TopicInfo { TopicId = o.TopicId, TopicName = o.TopicNews.Tag })
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
                    UrlNews = string.IsNullOrEmpty(x.UrlNews) ? _storageService.GetNewsUrl(x.DetailNews.Alias) : x.UrlNews,
                    LanguageId = x.LanguageId,
                    Timestamp = x.Timestamp
                }).ToListAsync();


            if (newsList == null)
                return new ApiErrorResult<List<NewsViewModel>>(400, "GetNewsByFollowedTopicUnsuccessful");

            return new ApiSuccessResult<List<NewsViewModel>>("GetNewsByFollowedTopicSuccessful", newsList);
        }
        catch(Exception ex)
        {
            return new ApiErrorResult<List<NewsViewModel>>(500, ex.Message);
        }
    }


    //Lấy tất cả các tin tức có trong chủ đề
    public async Task<ApiResult<List<NewsViewModel>>> GetNewsInTopic(int topicId)
    {
        try
        {
            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            query = query.Where(t => topicId == t.nit.TopicId);

            var data = await query
                .Select(x => new NewsViewModel
                {
                    NewsId = x.n.NewsId,
                    Title = x.n.Title,
                    OfficialRating = x.n.OfficialRating.ToString(),
                    Publisher = x.n.Publisher,
                    Status = x.n.Status,
                    TopicInfo = x.n.NewsInTopics.Select(o => new TopicInfo { TopicId = o.TopicId, TopicName = o.TopicNews.Tag })
                        .ToList(),
                    SocialBeliefs = x.n.SocialBeliefs,
                    ViewCount = x.n.ViewCount,
                    SourceCreate = x.n.SourceCreate.ToString(),
                    ThumbNews = string.IsNullOrEmpty(x.n.ImageLink)
                        ? _storageService.GetFileUrl(x.n.DetailNews.Media.PathMedia)
                        : x.n.ImageLink,
                    UrlNews = string.IsNullOrEmpty(x.n.UrlNews) ? _storageService.GetNewsUrl(x.n.DetailNews.Alias) : x.n.UrlNews,
                    LanguageId = x.n.LanguageId,
                    Timestamp = x.n.Timestamp
                }).ToListAsync();

            if (data == null) return new ApiErrorResult<List<NewsViewModel>>(400, "GetAllNewsInTopicUnsuccessful");

            if (data.Count == 0)
                return new ApiErrorResult<List<NewsViewModel>>(404, "DoNotHaveNewsInTopic");

            return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsInTopicSuccessful", data);
        }
        catch(Exception ex)
        {
            return new ApiErrorResult<List<NewsViewModel>>(404, ex.Message);
        }
        
    }
    //Tạo mới 1 tin tức (từ hệ thống tạo)
    public async Task<ApiResult<string>> CreateBySystem(NewsSystemCreateRequest request)
    {
        using (IDbContextTransaction transaction = _context.Database.BeginTransaction())

            try
            {
                //Kiểm tra ngôn ngữ có tồn tại
                var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);

                if (language == null) return new ApiErrorResult<string>(404, "LanguageNotFound");

                //Kiểm tra chủ đề có tồn tại
                foreach (var item in request.TopicId)
                {
                    var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                    if (topic == null)
                        return new ApiErrorResult<string>(404, "TopicNotFound");
                }
                LabelNews label_enum;

                if (Enum.TryParse<LabelNews>(request.OfficialRating.ToLower(), out label_enum))
                {
                    //Bắt đầu tạo tin tức
                    var news = new News
                    {
                        Title = request.Title,
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
                            ImageExtensions.Contains(Path.GetExtension(request.ThumbNews.FileName).ToUpperInvariant());

                        if (checkExtension == false)
                        {
                            return new ApiErrorResult<string>(400, "FileImageInvalid");
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
                            TopicId = topicId
                        });

                    var res = await _context.SaveChangesAsync();

                    if (res == 0)
                    {
                        transaction.Rollback();
                        await _storageService.DeleteFileAsync(detail_news.Media.PathMedia);
                        return new ApiErrorResult<string>(400, "CreateNewsUnsuccessful");
                    }

                    transaction.Commit();
                    return new ApiSuccessResult<string>("CreateNewsSuccessful", news.NewsId.ToString());
                }
                return new ApiErrorResult<string>(404, "OfficalRatingNotFound");

            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return new ApiErrorResult<string>(500, ex.Message);
            }
    }

    //Tạo mới 1 tin tức (từ nguồn bên ngoài
    public async Task<ApiResult<string>> CreateByOther(NewsOutSourceCreateRequest request)
    {
        using (IDbContextTransaction transaction = _context.Database.BeginTransaction())

            try
            {
                //Kiểm tra ngôn ngữ có tồn tại
                var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);

                if (language == null) return new ApiErrorResult<string>(404, "LanguageNotFound");

                //Kiểm tra chủ đề có tồn tại
                foreach (var item in request.TopicId)
                {
                    var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                    if (topic == null)
                        return new ApiErrorResult<string>(404, "TopicNotFound");
                }

                LabelNews label_enum;
                SourceCreate source_enum;

                if (Enum.TryParse<LabelNews>(request.OfficialRating.ToLower(), out label_enum))
                {
                    if (Enum.TryParse<SourceCreate>(request.SourceCreate, out source_enum))
                    {
                        //Trường hợp tạo tin tức từ nguồn bên ngoài (Có URL)
                        var news = new News
                        {
                            Title = request.Title,
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
                                TopicId = topicId
                            });

                        var res = await _context.SaveChangesAsync();

                        if (res == 0)
                        {
                            transaction.Rollback();
                            return new ApiErrorResult<string>(400, "CreateNewsUnsuccessful");
                        }

                        transaction.Commit();
                        return new ApiSuccessResult<string>("CreateNewsSuccessful", news.NewsId.ToString());
                    }
                    return new ApiErrorResult<string>(404, "SouceCreateNotFound");
                }
                return new ApiErrorResult<string>(404, "OfficalRatingNotFound");
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return new ApiErrorResult<string>(500, ex.Message);
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
                    await _storageService.DeleteFileAsync(media.PathMedia);
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
    public async Task<ApiResult<string>> UpdateByOutSource(NewsOutSourceUpdateRequest request)
    {
        using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
        {
            try
            {
                //Kiểm tra tin tức
                var news_update = await NewsCommon.CheckExistNews(_context, request.Id);

                if (news_update == null)
                    return new ApiErrorResult<string>(404, "CannontFindANewsWithId");

                //Trường hợp tin lấy từ nguồn ngoài (Không phải từ hệ thống tạo)
                if (news_update.DetailNews != null)
                {
                    return new ApiErrorResult<string>(400, "NewsCreateBySystem");
                }
                else {

                    news_update.Title = request.Title ?? news_update.Title;
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
                            return new ApiErrorResult<string>(404, "OfficalRatingNotFound");
                        }
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
                                return new ApiErrorResult<string>(404, "CannontFindATopicWithId");
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
                                TopicId = item
                            };

                            _context.NewsInTopics.Add(newsUpdate);
                        }
                    }

                    _context.News.Update(news_update);

                    var res = await _context.SaveChangesAsync();
                    if (res == 0)
                    {
                        transaction.Rollback();
                        return new ApiErrorResult<string>(400, "UpdateNewsUnsuccessful");
                    }

                    transaction.Commit();
                    return new ApiSuccessResult<string>("UpdateNewsSuccessful", " " + news_update.NewsId.ToString());
                }     
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return new ApiErrorResult<string>(500, ex.Message);
            }
        }
    }

    //Cập nhật tin tức (trừ Vote) bởi hệ thống
    public async Task<ApiResult<string>> UpdateBySystem(NewsSystemUpdateRequest request)
    {
        using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
        {
            try
            {
                //Kiểm tra tin tức
                var news_update = await NewsCommon.CheckExistNews(_context, request.Id);

                if (news_update == null)
                    return new ApiErrorResult<string>(404, "CannontFindANewsWithId");

                //Trường hợp tin tức không được tạo từ hệ thống
                if(news_update.DetailNews == null)
                {
                    return new ApiErrorResult<string>(400, "NewsCreateBySourceOther");
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
                            return new ApiErrorResult<string>(400, "FileImageInvalid");
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
                        if (thumb.PathMedia != null) await _storageService.DeleteFileAsync(thumb.PathMedia);
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
                        return new ApiErrorResult<string>(404, "OfficalRatingNotFound");
                    }
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
                            return new ApiErrorResult<string>(404, "CannontFindATopicWithId", " " + request.TopicId);
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
                            TopicId = item
                        };

                        _context.NewsInTopics.Add(newsUpdate);
                    }
                }

                var res = await _context.SaveChangesAsync();
                if (res == 0)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<string>(400, "UpdateNewsUnsuccessful");
                }

                transaction.Commit();
                return new ApiSuccessResult<string>("UpdateNewsSuccessful", " " + news_update.NewsId.ToString());
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return new ApiErrorResult<string>(500, ex.Message);
            }
        }
    }


    //Cập nhật đường dẫn tin tức
    public async Task<ApiResult<string>> UpdateLink(int newsId, string newLink)
    {
        try
        {
            var news_update = await NewsCommon.CheckExistNews(_context, newsId);

            if (news_update == null)
                return new ApiErrorResult<string>(404, "CannontFindANewsWithId", " " + newsId.ToString());

            news_update.UrlNews = newLink;

            var result = await _context.SaveChangesAsync();
            if (result == 0) return new ApiErrorResult<string>(400, "UpdateLinkNewsUnsuccessful");

            return new ApiSuccessResult<string>("UpdateLinkNewsSuccessful", newLink);
        }
        catch(Exception ex)
        {
            return new ApiErrorResult<string>(500, ex.Message);
        }
    }

    //Lưu file ảnh
    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        return fileName;
    }

    public async Task<ApiResult<string>> Archive(int Id)
    {
        try
        {
            var news = await NewsCommon.CheckExistNews(_context, Id);

            if (news == null)
                return new ApiErrorResult<string>(404, "CannontFindCommentWithId", " " + Id);

            news.Status = Status.Archive;

            var result = await _context.SaveChangesAsync();
            if (result == 0) return new ApiErrorResult<string>(400, "UpdateLinkNewsUnsuccessful");

            return new ApiSuccessResult<string>("UpdateLinkNewsSuccessful");
        }
        catch(Exception ex)
        {
            return new ApiErrorResult<string>(500, ex.Message);
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
        catch(Exception ex)
        {
            return new ApiErrorResult<bool>(500, ex.Message);
        }
        
    }

}