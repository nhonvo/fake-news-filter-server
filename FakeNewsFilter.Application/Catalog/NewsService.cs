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

    Task<ApiResult<List<NewsViewModel>>> GetNewsInTopic(int topicId);

    Task<ApiResult<List<NewsViewModel>>> GetNewsByFollowedTopic(List<int> topicList, Guid userId);

    Task<ApiResult<string>> Create(NewsCreateRequest request);

    Task<ApiResult<string>> Delete(int NewsId);

    Task<ApiResult<NewsViewModel>> GetById(int newsId);
    
    Task<ApiResult<NewsSystemViewModel>> GetContent(int newsId);

    Task<ApiResult<string>> Update(NewsUpdateRequest request);
    Task<ApiResult<string>> Archive(NewsUpdateRequest request);

    Task<ApiResult<string>> UpdateLink(int newsId, string newLink);


}

public class NewsService : INewsService
{
    public static readonly List<string> ImageExtensions = new() {".JPG", ".JPE", ".BMP", ".GIF", ".PNG"};
    private readonly ApplicationDBContext _context;

    private readonly IMapper _mapper;

    private readonly FileStorageService _storageService;

    private SlugHelper _slugHelper;

    public NewsService(ApplicationDBContext context, FileStorageService storageService, IMapper mapper, SlugHelper slugHelper)
    {
        _context = context;
        FileStorageService.USER_CONTENT_FOLDER_NAME = "images/news";
        _storageService = storageService;
        _mapper = mapper;
        _slugHelper = slugHelper;
    }

    //Lấy tất cả các tin tức (với Filter là lọc tin giả hay tin thật)
    public async Task<ApiResult<List<NewsViewModel>>> GetAll(string languageId, string filter)
    {
        var language = await LanguageCommon.CheckExistLanguage(_context, languageId);

        var newsList = new List<NewsViewModel>();

        if (string.IsNullOrEmpty(filter))

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
                            .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                        OfficialRating = x.OfficialRating,                     
                        Publisher = x.Publisher,
                        Status = x.Status,
                        ThumbNews = string.IsNullOrEmpty(x.ImageLink) ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia) : x.ImageLink,
                        URL = string.IsNullOrEmpty(x.Source) ? _storageService.GetNewsUrl(x.DetailNews.Alias) : x.Source,
                        LanguageId = x.LanguageId,
                        Timestamp = x.Timestamp
                    }
                ).ToListAsync();

        else if (string.IsNullOrEmpty(languageId))

            //Nếu bộ lọc không null và languageId là null
            newsList = await _context.News
                .Where(n => n.OfficialRating.ToLower().Contains(filter.ToLower()))
                .Select(x => new NewsViewModel
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    TopicInfo = x.NewsInTopics
                        .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                    OfficialRating = x.OfficialRating,
                    Publisher = x.Publisher,
                    Status = x.Status,
                    ThumbNews = string.IsNullOrEmpty(x.ImageLink) ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia) : x.ImageLink,
                    URL = string.IsNullOrEmpty(x.Source) ? _storageService.GetNewsUrl(x.DetailNews.Alias) : x.Source,
                    LanguageId = x.LanguageId,
                    Timestamp = x.Timestamp
                }).ToListAsync();
        else

            //Nếu bộ lọc không rỗng và languageId không rỗng
                newsList = await _context.News
                .Where(n => n.OfficialRating.ToLower().Contains(filter.ToLower()) && n.LanguageId == languageId)
                .Select(x => new NewsViewModel
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    TopicInfo = x.NewsInTopics
                        .Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag}).ToList(),
                    OfficialRating = x.OfficialRating,
                    Publisher = x.Publisher,
                    Status = x.Status,
                    ThumbNews = string.IsNullOrEmpty(x.ImageLink) ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia) : x.ImageLink,
                    URL = string.IsNullOrEmpty(x.Source) ? _storageService.GetNewsUrl(x.DetailNews.Alias) : x.Source,
                    LanguageId = x.LanguageId,
                    Timestamp = x.Timestamp
                }).ToListAsync();

        if (language == null) return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsSuccessful", newsList);

        return new ApiSuccessResult<List<NewsViewModel>>("GetAllNewsSuccessful", newsList);
    }

    //Lấy thông tin 1 tin tức thông qua Id
    public async Task<ApiResult<NewsViewModel>> GetById(int newsId)
    {
        var news = await _context.News
            .Include(t => t.NewsInTopics)
            .Include(t=>t.DetailNews)
            .FirstOrDefaultAsync(t => t.NewsId == newsId);


        if (news != null)
        {
            var topic = news.NewsInTopics.Select(o => new TopicInfo
            {
                TopicId = o.TopicId, TopicName = _context.TopicNews.FirstOrDefault(m => m.TopicId == o.TopicId).Tag
            }).ToList();

            NewsViewModel result = new NewsViewModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                OfficialRating = news.OfficialRating,
                Publisher = news.Publisher,
                ThumbNews = string.IsNullOrEmpty(news.ImageLink) ? _storageService.GetFileUrl(_context.Media.FirstOrDefault(x => x.MediaId == news.DetailNews.ThumbNews)?.PathMedia) : news.ImageLink,
                URL = string.IsNullOrEmpty(news.Source) ? _storageService.GetNewsUrl(news.DetailNews.Alias) : news.Source,
                LanguageId = news.LanguageId,
                Timestamp = news.Timestamp,
                Status = news.Status,
                TopicInfo = topic.ToList()
            };

            return new ApiSuccessResult<NewsViewModel>("GetThisNewsSuccessful", result);
        }

        return new ApiErrorResult<NewsViewModel>("NewsIsNotFound");
    }

    //Lấy nội dung 1 tin tức thông qua Id (chỉ khả dụng với tin được tạo từ hệ thống)
    public async Task<ApiResult<NewsSystemViewModel>> GetContent(int newsId)
    {
        var news = await _context.News
            .Include(t => t.NewsInTopics)
            .Include(t=>t.DetailNews)
            .FirstOrDefaultAsync(t => t.NewsId == newsId);

        //Trường hợp tin từ nguồn ngoài
        if(news.DetailNews == null)
        {
            return new ApiErrorResult<NewsSystemViewModel>("ContentNewsIsNotFound");
        }

        if (news != null)
        {
            var topic = news.NewsInTopics.Select(o => new TopicInfo
            {
                TopicId = o.TopicId, TopicName = _context.TopicNews.FirstOrDefault(m => m.TopicId == o.TopicId).Tag
            }).ToList();

            var checkMedia = _context.Media.FirstOrDefault(x => x.MediaId == news.DetailNews.ThumbNews)?.PathMedia;

            NewsSystemViewModel result = new NewsSystemViewModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                Content = news.DetailNews.Content,
                OfficialRating = news.OfficialRating,
                Publisher = news.Publisher,
                ThumbNews = _storageService.GetFileUrl(checkMedia),
                Alias = news.DetailNews.Alias,
                LanguageId = news.LanguageId,
                Timestamp = news.Timestamp,
                Status = news.Status,
                TopicInfo = topic.ToList()
            };

            return new ApiSuccessResult<NewsSystemViewModel>("GetThisNewsSuccessful", result);
        }

        return new ApiErrorResult<NewsSystemViewModel>("NewsIsNotFound");
    }
    
    //Lấy tin tức theo chủ đề mà người dùng theo dõi
    public async Task<ApiResult<List<NewsViewModel>>> GetNewsByFollowedTopic(List<int> topicList, Guid userId)
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
                TopicInfo = x.NewsInTopics.Select(o => new TopicInfo {TopicId = o.TopicId, TopicName = o.TopicNews.Tag})
                    .ToList(),
                OfficialRating = x.OfficialRating,
                Publisher = x.Publisher,
                Status = x.Status,
                ThumbNews = string.IsNullOrEmpty(x.ImageLink) ? _storageService.GetFileUrl(x.DetailNews.Media.PathMedia) : x.ImageLink,
                URL = string.IsNullOrEmpty(x.Source) ? _storageService.GetNewsUrl(x.DetailNews.Alias) : x.Source,
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
                 Title = x.n.Title,
                 OfficialRating = x.n.OfficialRating,
                 Publisher = x.n.Publisher,
                 Status = x.n.Status,
                 ThumbNews = string.IsNullOrEmpty(x.n.ImageLink) ? _storageService.GetFileUrl(x.n.DetailNews.Media.PathMedia) : x.n.ImageLink,
                 URL = string.IsNullOrEmpty(x.n.Source) ? _storageService.GetNewsUrl(x.n.DetailNews.Alias) : x.n.Source,
                 LanguageId = x.n.LanguageId,
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
                    //Kiểm tra ngôn ngữ có tồn tại
                    var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);

                    if (language == null) return new ApiErrorResult<string>("LanguageNotFound", " " + request.LanguageId);

                    //Kiểm tra chủ đề có tồn tại
                    foreach (var item in request.TopicId)
                    {
                        var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                        if (topic == null) return new ApiErrorResult<string>("TopicNotFound", " " + request.TopicId);
                    }

                    //Bắt buộc phải có 1 trong 2 trường (tin tức tự tạo hay nguồn bên ngoài)
                    if ((request.Content == null && request.Source == null) || (request.Content != null && request.Source != null))
                    {
                        return new ApiErrorResult<string>("NewsContentOrSourceInvalid");
                    }

                    //Không được bị conflict hình 2 bên (nguồn ngoài và hệ thống)
                    if (request.ThumbNews != null && request.ImageLink != null)
                    {
                        return new ApiErrorResult<string>("FileImageInvalid");
                    }

                    //Trường hợp tạo tin tức từ nguồn bên ngoài (Có URL)
                    if (request.Content == null && request.Source != null)
                    {
                    
                        //Tạo tin từ nguồn bên ngoài
                        var news = new News
                        {
                            Title = request.Title,
                            Source = request.Source,
                            ImageLink = request.ImageLink,
                            OfficialRating = request.OfficialRating,
                            DatePublished = request.DatePublished ?? DateTime.Now,
                            Publisher = request.Publisher,
                            IsVote = request.isVote,
                            LanguageId = request.LanguageId,
                            Timestamp = DateTime.Now
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
                            return new ApiErrorResult<string>("CreateNewsUnsuccessful", res);
                        }
                        transaction.Commit();
                        return new ApiSuccessResult<string>("CreateNewsSuccessful", news.NewsId.ToString());
                    }

                    //Trường hợp tạo tin tức từ hệ thống
                    else
                    {
                        //Bắt đầu tạo tin tức
                        var news = new News
                        {
                            Title = request.Title,
                            OfficialRating = request.OfficialRating,
                            DatePublished = request.DatePublished ?? DateTime.Now,
                            Publisher = request.Publisher,
                            IsVote = request.isVote,
                            LanguageId = request.LanguageId,
                            Timestamp = DateTime.Now,
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
                                return new ApiErrorResult<string>("FileImageInvalid", " " + checkExtension);
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
                            return new ApiErrorResult<string>("CreateNewsUnsuccessful", res);
                            
                        }
                        transaction.Commit();
                        return new ApiSuccessResult<string>("CreateNewsSuccessful", news.NewsId.ToString());

                }
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
        //Kiểm tra tin có tồn tại không trước khi xoá
        var news = await NewsCommon.CheckExistNews(_context, newsId);

        if (news == null)
        {
            return new ApiErrorResult<string>("CannontFindANewsWithId", newsId);

        }

        if(news.DetailNews != null)
        {
            //Xoá ảnh trước
            if(news.DetailNews.ThumbNews != null)
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

        if (result == 0) return new ApiErrorResult<string>("DeleteNewsUnsuccessful"," " + result);

        return new ApiSuccessResult<string>("DeleteNewsSuccessful", " " + news.NewsId.ToString());
        }


    //Cập nhật tin tức (trừ Vote)
    public async Task<ApiResult<string>> Update(NewsUpdateRequest request)
    {
        using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
        {
            try
            {
                //Kiểm tra tin tức
                var news_update = await NewsCommon.CheckExistNews(_context, request.Id);

                if (news_update == null)
                    return new ApiErrorResult<string>("CannontFindANewsWithId", request.Id);

                //Trường hợp tin lấy từ nguồn ngoài (Không phải từ hệ thống tạo)
                if(news_update.DetailNews == null)
                {
                    news_update.Title = request.Title ?? news_update.Title;
                    news_update.LanguageId = request.LanguageId ?? news_update.LanguageId;
                    news_update.ImageLink = request.ImageLink ?? news_update.ImageLink;
                    news_update.Source = request.Source ?? news_update.Source;
                    news_update.OfficialRating = request.OfficialRating ?? news_update.OfficialRating;
                    news_update.Publisher = request.Publisher ?? news_update.Publisher;
                    news_update.LanguageId = request.LanguageId ?? news_update.LanguageId;
                    news_update.Timestamp = DateTime.Now;

                    //Tìm các Topic của News đó
 
                    var newsListCanDelete = news_update.NewsInTopics.Select(x=>x.TopicId).ToList();

                    //So sánh có sự thay đổi chủ đề của tin tức đó không?
                    var compareTopic = Enumerable.SequenceEqual(request.TopicId, newsListCanDelete);

                    if(compareTopic == false)
                    {
                        //Kiểm tra id chủ đề
                        foreach (var item in request.TopicId)
                        {
                            var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                            if (topic == null) return new ApiErrorResult<string>("CannontFindATopicWithId", " " + request.TopicId);
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
                        return new ApiErrorResult<string>("UpdateNewsUnsuccessful", " " + res);
                    }
                    transaction.Commit();
                    return new ApiSuccessResult<string>("UpdateNewsSuccessful", " " + news_update.NewsId.ToString());

                }
                //Trường hợp cập nhật tin tự tạo
                else
                {
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
                                ImageExtensions.Contains(Path.GetExtension(request.ThumbNews.FileName).ToUpperInvariant());

                            if (checkExtension == false)
                            {
                                return new ApiErrorResult<string>("FileImageInvalid", checkExtension.ToString());
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
                    news_update.OfficialRating = request.OfficialRating ?? news_update.OfficialRating;
                    news_update.Publisher = request.Publisher ?? news_update.Publisher;
                    news_update.LanguageId = request.LanguageId ?? news_update.LanguageId;
                    news_update.Timestamp = DateTime.Now;

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
                            if (topic == null) return new ApiErrorResult<string>("CannontFindATopicWithId", " " + request.TopicId);
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
                        return new ApiErrorResult<string>("UpdateNewsUnsuccessful", " " + res);
                    }
                    transaction.Commit();
                    return new ApiSuccessResult<string>("UpdateNewsSuccessful", " " + news_update.NewsId.ToString());

                }
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
        var news_update = await NewsCommon.CheckExistNews(_context, newsId);

        if (news_update == null)
            return new ApiErrorResult<string>("CannontFindANewsWithId", newsId);

        news_update.Source = newLink;

        var result = await _context.SaveChangesAsync();
        if (result == 0) return new ApiErrorResult<string>("UpdateLinkNewsUnsuccessful", result);

        return new ApiSuccessResult<string>("UpdateLinkNewsSuccessful", newLink);
    }

    //Lưu file ảnh
    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        return fileName;
    }

    public async Task<ApiResult<string>> Archive(NewsUpdateRequest request)
    {
        var news = await NewsCommon.CheckExistNews(_context, request.Id);

        if (news == null)
            return new ApiErrorResult<string>("CannontFindCommentWithId", request.Id);

        news.Status = Status.Archive;

        var result = await _context.SaveChangesAsync();
        if (result == 0) return new ApiErrorResult<string>("UpdateLinkNewsUnsuccessful", result);

        return new ApiSuccessResult<string>("UpdateLinkNewsSuccessful");
    }
}