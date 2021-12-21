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
                    Content = news.Content,
                    DatePublished = news.DatePublished,
                    LanguageId = news.LanguageId,
                    ThumbNews = news.Media.PathMedia,
                };

                return new ApiSuccessResult<NewsCommunityViewModel>("GetThisNewsSuccessful", result);
            }

            return new ApiErrorResult<NewsCommunityViewModel>("NewsIsNotFound");
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
