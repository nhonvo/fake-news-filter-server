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
        Task<ApiResult<int>> Create(StoryCreateRequest request);
        Task<ApiResult<bool>> Update(StoryUpdateRequest request);
        Task<ApiResult<bool>> Delete(int StoryId);
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

        public async Task<ApiResult<int>> Create(StoryCreateRequest request)
        {
            //check LanguageId
            var language = await _context.Languages.FirstOrDefaultAsync(x => x.Id == request.LanguageId);
            if (language == null)
            {
                return new ApiErrorResult<int>("LanguageIdNotFound");
            }
            //check SourceId
            var source = await _context.Source.FindAsync(request.SourceId);
            if (source == null)
            {
                return new ApiErrorResult<int>("SourceIdNotFound");
            }
            //add item in story
            var story = new Story()
            {
                Timestamp = DateTime.Now,
                Link = request.Link,
                SourceId = request.SourceId,
                LanguageId = request.LanguageId
            };
            //Save Image on Host
            if (request.ThumbStory != null)
            {
                //Kiểm tra định dạng file đưa vào
                var checkExtension =
                    ImageExtensions.Contains(Path.GetExtension(request.ThumbStory.FileName).ToUpperInvariant());

                if (checkExtension == false)
                {
                    return new ApiErrorResult<int>("FileImageInvalid");
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
                return new ApiErrorResult<int>("CreateStoryUnsuccessful");
            }

            return new ApiSuccessResult<int>("CreateStorySuccessful", story.StoryId);

        }
        public async Task<ApiResult<bool>> Update(StoryUpdateRequest request)
        {
            //check LanguageId
            var language_update = await _context.Languages.FirstOrDefaultAsync(x => x.Id == request.LanguageId);
            if (language_update == null)
            {
                return new ApiErrorResult<bool>("LanguageIdNotFound");
            }
            //Check StoryId
            var story_update = await _context.Story.FindAsync(request.StoryId);

            if (story_update == null)
                return new ApiErrorResult<bool>($"CannontFindAStoryWithId");

            //checkSourceId
            var source_update = await _context.Source.FindAsync(request.SourceId);

            if (source_update == null)
                return new ApiErrorResult<bool>($"CannontFindASourceWithId");

            //update link, sourceId
            story_update.Link = request.Link ?? story_update.Link;

            story_update.SourceId = request?.SourceId ?? story_update.SourceId;

            story_update.LanguageId = request.LanguageId ?? story_update.LanguageId;

            //Update image
            if (request.ThumbStory != null)
            {
                //Check image
                var thumb = _context.Media.FirstOrDefault(i => i.MediaId == story_update.Thumbstory);

                //If image null, create new image
                if (thumb == null)
                {
                    //Kiểm tra định dạng file đưa vào
                    var checkExtension =
                        ImageExtensions.Contains(Path.GetExtension(request.ThumbStory.FileName).ToUpperInvariant());

                    if (checkExtension == false)
                    {
                        return new ApiErrorResult<bool>("FileImageInvalid");
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

            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("UpdateStoryUnsuccessful");
            }

            return new ApiSuccessResult<bool>("UpdateStorySuccessful", false);
        }
        public async Task<ApiResult<bool>> Delete(int StoryId)
        {
            try
            {
                var story = await _context.Story.FindAsync(StoryId);

                if (story == null) throw new FakeNewsException("CannontFindAStoryWithId");

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
                    return new ApiSuccessResult<bool>("DeleteStorySuccessful", false);
                }

                return new ApiErrorResult<bool>("DeleteStoryUnsuccessful");
            }

            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }
        public async Task<ApiResult<StoryViewModel>> GetOneStory(int storyId)
        {
            try
            {
                var story = await _context.Story.SingleOrDefaultAsync(x => x.StoryId == storyId && x.Timestamp >= DateTime.Now.AddHours(-24));

                if (story == null)
                {
                    return new ApiErrorResult<StoryViewModel>("StoryNotFound");
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
                return new ApiErrorResult<List<StoryViewModel>>("GetAllStoryUnsuccessful");
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
    }
}
