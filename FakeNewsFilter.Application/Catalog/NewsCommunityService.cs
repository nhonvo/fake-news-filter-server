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

namespace FakeNewsFilter.Application.Catalog
{
    public interface INewsCommunityService
    {
        Task<ApiResult<int>> Create(NewsCommunityCreateRequest request);
        Task<ApiResult<NewsCommunityViewModel>> GetById(int newsCommunityId);
        Task<ApiResult<bool>> Delete(int NewsCommunityId);
        Task<ApiResult<bool>> Update(NewsCommunityUpdateRequest request);
        Task<ApiResult<bool>> UpdateLink(int newsCommunityId, string newLink);
        Task<ApiResult<List<NewsCommunityViewModel>>> GetAll(string languageId);
    }
    public class NewsCommunityService : INewsCommunityService
    {
        public static readonly List<string> ImageExtensions = new() { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        private readonly ApplicationDBContext _context;

        private readonly IMapper _mapper;

        private readonly FileStorageService _storageService;
        public NewsCommunityService(ApplicationDBContext context, FileStorageService storageService, IMapper mapper)
        {
            _context = context;
            FileStorageService.USER_CONTENT_FOLDER_NAME = "images/newscommunities";
            _storageService = storageService;
            _mapper = mapper;
        }

        //Tạo mới 1 tin tức
        public async Task<ApiResult<int>> Create(NewsCommunityCreateRequest request)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())

                try
                {
                    //check language
                    var language = await _context.Languages.FirstOrDefaultAsync(x => x.Id == request.LanguageId);
                    if (language == null) return new ApiErrorResult<int>("LanguageNotFound");

                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
                    if (user == null) return new ApiErrorResult<int>("UserNotFound");
                    
                    var newsCommunity = new NewsCommunity
                    {
                        Title = request.Title,
                        Content = request.Content,
                        IsPopular = request.IsPopular,
                        UserId = request.UserId,
                        DatePublished = DateTime.Now,
                        LanguageId = request.LanguageId
                    };

                    //Save Image on Host
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

                    _context.newsCommunities.Add(newsCommunity);

                    if (await _context.SaveChangesAsync() == 0)
                    {
                        transaction.Rollback();
                        await _storageService.DeleteFileAsync(newsCommunity.Media.PathMedia);
                        return new ApiErrorResult<int>("CreateNewsUnsuccessful");
                    }

                    transaction.Commit();
                    return new ApiSuccessResult<int>("CreateNewsSuccessful", newsCommunity.NewsCommunityId);
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<int>(ex.Message);
                }

        }

        //Lấy thông tin News thông qua Id
        public async Task<ApiResult<NewsCommunityViewModel>> GetById(int newsCommunityId)
        {
            var news = await _context.newsCommunities.FirstOrDefaultAsync(t => t.NewsCommunityId == newsCommunityId);

            NewsCommunityViewModel result = null;

            if (news != null)
            {
                var media = _context.Media.Where(x => x.MediaId == news.ThumbNews).FirstOrDefault();

                result = new NewsCommunityViewModel
                {
                    NewsCommunityId = news.NewsCommunityId,
                    UserId = news.UserId,
                    Title = news.Title,
                    IsPopular = news.IsPopular,
                    Content = news.Content,
                    DatePublished = news.DatePublished,
                    LanguageId = news.LanguageId,
                    ThumbNews = string.IsNullOrEmpty(news.Media?.PathMedia) ? null : media.PathMedia,
                };

                return new ApiSuccessResult<NewsCommunityViewModel>("GetThisNewsSuccessful", result);
            }

            return new ApiErrorResult<NewsCommunityViewModel>("NewsIsNotFound");
        }

        //Xoá tin tức
        public async Task<ApiResult<bool>> Delete(int newsCommunityId)
        {
            var news = await _context.newsCommunities.FindAsync(newsCommunityId);

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

            _context.newsCommunities.Remove(news);

            if (await _context.SaveChangesAsync() == 0) return new ApiErrorResult<bool>("DeleteNewsUnsuccessful");

            return new ApiSuccessResult<bool>("DeleteNewsSuccessful", false);
        }

        //Cập nhật tin tức
        public async Task<ApiResult<bool>> Update(NewsCommunityUpdateRequest request)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var news_update = await _context.newsCommunities.FirstOrDefaultAsync(x => x.NewsCommunityId == request.NewsCommunityId);

                    if (news_update == null)
                        return new ApiErrorResult<bool>("CannontFindANewsWithId");

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
                        _context.newsCommunities.Update(news_update);
                    }
                    

                    if (await _context.SaveChangesAsync() == 0)
                    {
                        transaction.Rollback();
                        return new ApiErrorResult<bool>("UpdateNewsUnsuccessful");
                    }

                    transaction.Commit();
                    return new ApiSuccessResult<bool>("UpdateNewsSuccessful", false);
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<bool>(ex.Message);
                }

            }


        }

        //Get All News
        public async Task<ApiResult<List<NewsCommunityViewModel>>> GetAll(string languageId)
        {
            var language = await _context.Languages.SingleOrDefaultAsync(x => x.Id == languageId);

            var newsList = new List<NewsCommunityViewModel>();
                newsList = await _context.newsCommunities.Where(n => n.LanguageId == languageId)
                    .Select(x => new NewsCommunityViewModel
                    {
                        NewsCommunityId = x.NewsCommunityId,
                        Title = x.Title,
                        Content = x.Content,
                        UserId = x.UserId,
                        ThumbNews = x.Media.PathMedia,
                        LanguageId = x.LanguageId,
                        IsPopular = x.IsPopular,
                        DatePublished = x.DatePublished
                    }).ToListAsync();

            if (language == null) return new ApiSuccessResult<List<NewsCommunityViewModel>>("GetAllNewsSuccessful", newsList);

            return new ApiSuccessResult<List<NewsCommunityViewModel>>("GetAllNewsSuccessful", newsList);
        }

        //Update Link News
        public async Task<ApiResult<bool>> UpdateLink(int newsCommunityId, string newLink)
        {
            var news_update = await _context.newsCommunities.FindAsync(newsCommunityId);

            if (news_update == null)
                return new ApiErrorResult<bool>("CannontFindANewsWithId");

            news_update.Content = newLink;

            if (await _context.SaveChangesAsync() == 0) return new ApiErrorResult<bool>("UpdateLinkNewsUnsuccessful");

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
