using AutoMapper;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.ViewModel.Catalog.NewsCommunity;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.System.Users;
using Microsoft.AspNetCore.Identity;

namespace FakeNewsFilter.Application.Catalog
{
    public interface INewsCommunityService
    {
        Task<ApiResult<string>> Create(NewsCommunityCreateRequest request);
        Task<ApiResult<NewsCommunityViewModel>> GetById(int newsCommunityId);
        Task<ApiResult<string>> Delete(int NewsCommunityId);
        Task<ApiResult<string>> Update(NewsCommunityUpdateRequest request);
        Task<ApiResult<string>> Archive(NewsCommunityUpdateRequest request);
        Task<ApiResult<string>> UpdateLink(int newsCommunityId, string newLink);
        Task<ApiResult<List<NewsCommunityViewModel>>> GetAll(string languageId);
        Task<ApiResult<List<NewsCommunityViewModel>>> GetNewsByUserId(Guid userId);
    }

    public class NewsCommunityService : INewsCommunityService
    {
        public static readonly List<string> ImageExtensions = new() {".JPG", ".JPE", ".BMP", ".GIF", ".PNG"};
        private readonly ApplicationDBContext _context;

        private readonly IMapper _mapper;

        private readonly FileStorageService _storageService;

        private readonly UserManager<User> _userManager;

        public NewsCommunityService(UserManager<User> userManager, ApplicationDBContext context, FileStorageService storageService, IMapper mapper)
        {
            _context = context;
            FileStorageService.USER_CONTENT_FOLDER_NAME = "images/newscommunities";
            _storageService = storageService;
            _mapper = mapper;
            _userManager = userManager;
        }



        //Tạo mới 1 tin tức
        public async Task<ApiResult<string>> Create(NewsCommunityCreateRequest request)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())

                try
                {
                    var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);
                    if (language == null) return new ApiErrorResult<string>(404, "LanguageNotFound", " " + request.LanguageId);

                    var user = await UserCommon.CheckExistUser(_context, request.UserId);
                    if (user == null) return new ApiErrorResult<string>(404, "UserNotFound", " " + request.UserId);

                    var newsCommunity = new NewsCommunity
                    {
                        Title = request.Title,
                        Content = request.Content,
                        IsPopular = request.IsPopular,
                        UserId = request.UserId,
                        DatePublished = DateTime.Now,
                        LanguageId = request.LanguageId
                    };

                    //Lưu ảnh vào  máy chủ
                    if (request.ThumbNews != null)
                    {
                        var checkExtension =
                            ImageExtensions.Contains(Path.GetExtension(request.ThumbNews.FileName).ToUpperInvariant());
                        newsCommunity.Media = new Media
                        {
                            DateCreated = DateTime.Now,
                            FileSize = request.ThumbNews.Length,
                            PathMedia = await SaveFile(request.ThumbNews),
                            Type = checkExtension ? MediaType.Image : MediaType.Video,
                            Caption = "Thumb News " + (checkExtension ? "Image" : "Video")
                        };
                    }

                    _context.NewsCommunity.Add(newsCommunity);
                    var result = await _context.SaveChangesAsync();
                    if (result == 0)
                    {
                        transaction.Rollback();
                        await _storageService.DeleteFileAsync(newsCommunity.Media.PathMedia);
                        return new ApiErrorResult<string>(400, "CreateNewsUnsuccessful");
                    }

                    transaction.Commit();
                    return new ApiSuccessResult<string>("CreateNewsSuccessful", newsCommunity.NewsCommunityId.ToString());
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<string>(500, ex.Message);
                }
        }

        //Lấy thông tin News thông qua Id
        public async Task<ApiResult<NewsCommunityViewModel>> GetById(int newsCommunityId)
        {
            var news = await NewscommunityCommon.CheckExistNews(_context, newsCommunityId);

            NewsCommunityViewModel result = null;

            if (news != null)
            {
                var media = _context.Media.Where(x => x.MediaId == news.ThumbNews).FirstOrDefault();
                
                var user = _context.Users.Where(x => x.Id == news.UserId).FirstOrDefault();
                
                result = new NewsCommunityViewModel
                {
                    NewsCommunityId = news.NewsCommunityId,
                    Title = news.Title,
                    IsPopular = news.IsPopular,
                    Content = news.Content,
                    Publisher = new UserViewModel()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        FullName = user.Name,
                        Status = user.Status,
                        UserName = user.UserName,
                        noNewsContributed = _context.NewsCommunity.Count(i => i.UserId == user.Id),
                    },
                    DatePublished = news.DatePublished,
                    LanguageId = news.LanguageId,
                    ThumbNews = string.IsNullOrEmpty(news.Media?.PathMedia) ? null : media.PathMedia,
                };

                return new ApiSuccessResult<NewsCommunityViewModel>("GetThisNewsSuccessful", result);
            }

            return new ApiErrorResult<NewsCommunityViewModel>(404, "NewsIsNotFound");
        }

        //Xoá tin tức
        public async Task<ApiResult<string>> Delete(int newsCommunityId)
        {
            var news = await NewscommunityCommon.CheckExistNews(_context, newsCommunityId);

            if (news == null)
                return new ApiErrorResult<string>(404, "CannontFindANewsWithId", " " + newsCommunityId.ToString());

            if (news.ThumbNews != null)
            {
                var media = _context.Media.Single(x => x.MediaId == news.ThumbNews);

                if (media != null && media.PathMedia != null)
                {
                    await _storageService.DeleteFileAsync(media.PathMedia);
                    _context.Media.Remove(media);
                }
            }

            _context.NewsCommunity.Remove(news);
            var result = await _context.SaveChangesAsync();
            if (result == 0) return new ApiErrorResult<string>(400, "DeleteNewsUnsuccessful", " " + result);

            return new ApiSuccessResult<string>("DeleteNewsSuccessful"," " + news.NewsCommunityId.ToString());
        }

        //Cập nhật tin tức
        public async Task<ApiResult<string>> Update(NewsCommunityUpdateRequest request)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var news_update = await NewscommunityCommon.CheckExistNews(_context, request.NewsCommunityId);

                    if (news_update == null)
                        return new ApiErrorResult<string>(404, "CannontFindANewsWithId", " " + request.NewsCommunityId);

                    news_update.Title = request.Title ?? news_update.Title;
                    news_update.Content = request.Content ?? news_update.Content;
                    news_update.LanguageId = request.LanguageId ?? news_update.LanguageId;
                    news_update.IsPopular = request.IsPopular;
                    news_update.DatePublished = DateTime.Now;

                    if (request.ThumbNews != null)
                    {
                        //Kiểm tra hình đã có trên DB chưa
                        var thumb = _context.Media.FirstOrDefault(i => i.MediaId == news_update.ThumbNews);

                        //Nếu chưa có hình thì thêm hình mới
                        if (thumb == null)
                        {
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

                        _context.NewsCommunity.Update(news_update);
                    }

                    var result = await _context.SaveChangesAsync();
                    if (result == 0)
                    {
                        transaction.Rollback();
                        return new ApiErrorResult<string>(400, "UpdateNewsUnsuccessful"," " + result);
                    }

                    transaction.Commit();
                    return new ApiSuccessResult<string>("UpdateNewsSuccessful", " " + news_update.NewsCommunityId.ToString());
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<string>(500, ex.Message);
                }
            }
        }

        //Lấy tất cả các tin tức
        public async Task<ApiResult<List<NewsCommunityViewModel>>> GetAll(string languageId)
        {
            var language = await LanguageCommon.CheckExistLanguage(_context, languageId);

            var query = from n in _context.NewsCommunity
                join user in _context.Users on n.UserId equals user.Id
                select new {n, user};

            query = query.Where(t => languageId == t.n.LanguageId);

            var newsList = await query
                .Select(x => new NewsCommunityViewModel()
                {
                    NewsCommunityId = x.n.NewsCommunityId,
                    Title = x.n.Title,
                    Content = x.n.Content,
                    Publisher = new UserViewModel()
                    {
                        UserId = x.user.Id,
                        Email = x.user.Email,
                        FullName = x.user.Name,
                        Status = x.user.Status,
                        UserName = x.user.UserName,
                        noNewsContributed = _context.NewsCommunity.Count(i => i.UserId == x.user.Id),
                    },
                    ThumbNews = x.n.Media.PathMedia,
                    LanguageId = x.n.LanguageId,
                    IsPopular = x.n.IsPopular,
                    DatePublished = x.n.DatePublished,
                }).ToListAsync();
            if (language == null)
                return new ApiSuccessResult<List<NewsCommunityViewModel>>("GetAllNewsSuccessful", newsList);

            return new ApiSuccessResult<List<NewsCommunityViewModel>>("GetAllNewsSuccessful", newsList);
        }

        //Lấy tin tức được đăng bời người dùng
        public async Task<ApiResult<List<NewsCommunityViewModel>>> GetNewsByUserId(Guid userId)
        {
            try
            {
                var query = from n in _context.NewsCommunity
                            join c in _context.Users on n.UserId equals c.Id
                            select new { n, c };

                query = query.Where(t => userId == t.c.Id);


                var newsList = await query
                    .Select(x => new NewsCommunityViewModel
                    {
                        NewsCommunityId = x.n.NewsCommunityId,
                        Title = x.n.Title,
                        Content = x.n.Content,
                        ThumbNews = x.n.Media.PathMedia,
                        LanguageId = x.n.LanguageId,
                        IsPopular = x.n.IsPopular,
                        DatePublished = x.n.DatePublished,
                    }).ToListAsync();

                if (newsList == null)
                    return new ApiErrorResult<List<NewsCommunityViewModel>>(400, "GetNewsByIdUnsuccessful");
                if (newsList.Count == 0)
                    return new ApiErrorResult<List<NewsCommunityViewModel>>(404, "DoNotHaveNewsOfUser");
                return new ApiSuccessResult<List<NewsCommunityViewModel>>("GetNewsByIdSuccessful", newsList);

            }
            catch (Exception)
            {

                throw;
            }
        }
        //Cập nhật đường dẫn tin tức
        public async Task<ApiResult<string>> UpdateLink(int newsCommunityId, string newLink)
        {
            var news_update = await NewscommunityCommon.CheckExistNews(_context, newsCommunityId);

            if (news_update == null)
                return new ApiErrorResult<string>(404, "CannontFindANewsWithId", " " + newsCommunityId.ToString());

            news_update.Content = newLink;

            var result = await _context.SaveChangesAsync();
            if (result == 0) return new ApiErrorResult<string>(400, "UpdateLinkNewsUnsuccessful");

            return new ApiSuccessResult<string>("UpdateLinkNewsSuccessful", newLink);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ApiResult<string>> Archive(NewsCommunityUpdateRequest request)
        {
            var news_community = await NewscommunityCommon.CheckExistNews(_context, request.NewsCommunityId);

            if (news_community == null)
                return new ApiErrorResult<string>(404, "CannontFindCommentWithId", " " + request.NewsCommunityId);

            news_community.Status = Status.Archive;

            var result = await _context.SaveChangesAsync();
            if (result == 0) return new ApiErrorResult<string>(400, "UpdateLinkNewsUnsuccessful");

            return new ApiSuccessResult<string>("UpdateLinkNewsSuccessful");
        }
    }
}