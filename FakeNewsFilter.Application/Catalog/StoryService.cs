using AutoMapper;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Story;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FakeNewsFilter.Application.Catalog
{
    public interface IStoryService
    {
        Task<ApiResult<string>> Create(StoryCreateRequest request);
        Task<ApiResult<string>> Update(StoryUpdateRequest request);
        Task<ApiResult<string>> Archive(StoryUpdateRequest request);
        Task<ApiResult<string>> Delete(int StoryId);
        Task<ApiResult<StoryViewModel>> GetOneStory(int StoryId);
        Task<ApiResult<List<StoryViewModel>>> GetAllStory(string languageId);
    }
    public class StoryService : IStoryService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly FileStorageService _storageService;
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public StoryService(ApplicationDBContext context, FileStorageService storageService, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _storageService = storageService;
            FileStorageService.USER_CONTENT_FOLDER_NAME = "images/stories";
        }

        //tạo một story mới
        public async Task<ApiResult<string>> Create(StoryCreateRequest request)
        {
            //Kiểm tra ngôn ngữ có tồn tại hay không
            var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);
            if (language == null)
            {
                return new ApiErrorResult<string>("LanguageIdNotFound"," " + request.LanguageId);
            }
            //kiểm tra nguồn có tồn tại hay không
            var source = await SourceCommon.CheckExistSource(_context, request.SourceId);
            if (source == null)
            {
                return new ApiErrorResult<string>("SourceIdNotFound", " " + request.SourceId.ToString());
            }
            
            var story = new Story()
            {
                Timestamp = DateTime.Now,
                Link = request.Link,
                SourceId = request.SourceId,
                LanguageId = request.LanguageId
            };
            // Lưu hình ảnh trên máy chủ lưu trữ
            if (request.ThumbStory != null)
            {
                //Kiểm tra định dạng file đưa vào
                var checkExtension =
                    ImageExtensions.Contains(Path.GetExtension(request.ThumbStory.FileName).ToUpperInvariant());

                if (checkExtension == false)
                {
                    return new ApiErrorResult<string>("FileImageInvalid", " " + checkExtension.ToString());
                }

                story.Media = new Media()
                {
                    Caption = "Thumbnail Story",
                    DateCreated = DateTime.Now,
                    FileSize = request.ThumbStory.Length,
                    PathMedia = await this.SaveFile(request.ThumbStory),
                    Type = MediaType.Image,
                    SortOrder = 1
                };
            }

            _context.Story.Add(story);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                await _storageService.DeleteFileAsync(story.Media.PathMedia);
                return new ApiErrorResult<string>("CreateStoryUnsuccessful", " " + result.ToString());
            }

            return new ApiSuccessResult<string>("CreateStorySuccessful", " " + story.StoryId.ToString());

        }
        //Cập nhật story
        public async Task<ApiResult<string>> Update(StoryUpdateRequest request)
        {
            //Kiểm tra ngôn ngữ có tồn tại hay không
            var language_update = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);
            if (language_update == null)
            {
                return new ApiErrorResult<string>("LanguageIdNotFound"," " + request.LanguageId);
            }
            //Kiểm tra story có tồn tại hay không
            var story_update = await StoryCommon.CheckExistStory(_context, request.StoryId);

            if (story_update == null)
                return new ApiErrorResult<string>($"CannontFindAStoryWithId", " " + request.StoryId);

            //Kiểm tra nguồn có tồn tại hay không
            var source_update = await SourceCommon.CheckExistSource(_context, request.SourceId);

            if (source_update == null)
                return new ApiErrorResult<string>($"CannontFindASourceWithId", " " + request.SourceId);

            //Cập nhật đường dẫn và nguồn mới
            story_update.Link = request.Link ?? story_update.Link;

            story_update.SourceId = request?.SourceId ?? story_update.SourceId;

            story_update.LanguageId = request.LanguageId ?? story_update.LanguageId;

            //Cập nhật ảnh
            if (request.ThumbStory != null)
            {
                //Kiểm tra ảnh
                var thumb = _context.Media.FirstOrDefault(i => i.MediaId == story_update.Thumbstory);

                //Nếu ảnh null thì tạo ảnh mới
                if (thumb == null)
                {
                    //Kiểm tra định dạng file đưa vào
                    var checkExtension =
                        ImageExtensions.Contains(Path.GetExtension(request.ThumbStory.FileName).ToUpperInvariant());

                    if (checkExtension == false)
                    {
                        return new ApiErrorResult<string>("FileImageInvalid", " " + checkExtension.ToString());
                    }

                    story_update.Media = new Media()
                    {
                        Caption = "Thumbnail Story",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbStory.Length,
                        PathMedia = await this.SaveFile(request.ThumbStory),
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
                    thumb.FileSize = request.ThumbStory.Length;
                    thumb.PathMedia = await SaveFile(request.ThumbStory);

                    thumb.Type = MediaType.Image;

                    _context.Media.Update(thumb);
                }
            }
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return new ApiErrorResult<string>("UpdateStoryUnsuccessful", " " + result.ToString());
            }

            return new ApiSuccessResult<string>("UpdateStorySuccessful", " " + story_update.ToString());
        }

        //Xóa story
        public async Task<ApiResult<string>> Delete(int StoryId)
        {
            try
            {
                var story = await StoryCommon.CheckExistStory(_context, StoryId);

                if (story == null) return new ApiSuccessResult<string>("CannontFindAStoryWithId"," " + StoryId.ToString());

                var media = _context.Media.FirstOrDefault(i => i.MediaId == story.Thumbstory);

                if (media != null)
                {
                    if (media.PathMedia != null)
                        await _storageService.DeleteFileAsync(media.PathMedia);
                    _context.Media.Remove(media);
                }

                _context.Story.Remove(story);

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<string>("DeleteStorySuccessful", " " + story.StoryId.ToString());
                }

                return new ApiErrorResult<string>("DeleteStoryUnsuccessful", " " + result.ToString());
            }

            catch (Exception ex)
            {
                return new ApiErrorResult<string>(ex.Message);
            }
        }

        //Lấy một story
        public async Task<ApiResult<StoryViewModel>> GetOneStory(int storyId)
        {
            try
            {
                var story = await _context.Story.SingleOrDefaultAsync(x => x.StoryId == storyId && x.Timestamp >= DateTime.Now.AddHours(-24));

                if (story == null)
                {
                    return new ApiErrorResult<StoryViewModel>("StoryNotFound", storyId);
                }
                var storyVM = new StoryViewModel
                {
                    StoryId = story.StoryId,
                    Link = story.Link,
                    SourceId = story.SourceId,
                    Thumbstory = story.Thumbstory,
                    SyncTime = DateTime.Now - story.Timestamp,
                    LanguageId = story.LanguageId
                };

                return new ApiSuccessResult<StoryViewModel>("GetStorySuccessful", storyVM);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<StoryViewModel>(ex.Message);
            }
        }

        //Lấy tất cả story trong hệ thống
        public async Task<ApiResult<List<StoryViewModel>>> GetAllStory(string languageId)
        {
            var list_story = await _context.Story.Where(n => !string.IsNullOrEmpty(languageId) ? n.LanguageId == languageId : true && n.Timestamp >= DateTime.Now.AddHours(-24))
                .Select(x => new StoryViewModel()
                {
                    SourceId = x.SourceId,
                    StoryId = x.StoryId,
                    LanguageId = x.LanguageId,
                    SyncTime = DateTime.Now - x.Timestamp,
                    Thumbstory = x.Thumbstory,
                    Link = x.Link
                }).ToListAsync();

            if (list_story == null)
            {
                return new ApiErrorResult<List<StoryViewModel>>("GetAllStoryUnsuccessful", list_story);
            }

            return new ApiSuccessResult<List<StoryViewModel>>("GetAllStorySuccessful", list_story);
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            //lấy ra tên của file ảnh
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            //đặt tên lại cho file ảnh đó
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ApiResult<string>> Archive(StoryUpdateRequest request)
        {
            var story = await StoryCommon.CheckExistStory(_context, request.StoryId);

            if (story == null)
                return new ApiErrorResult<string>("CannontFindCommentWithId", request.StoryId);

            story.Status = Status.Archive;

            var result = await _context.SaveChangesAsync();
            if (result == 0) return new ApiErrorResult<string>("UpdateLinkNewsUnsuccessful", result);

            return new ApiSuccessResult<string>("UpdateLinkNewsSuccessful");
        }
    }
}
