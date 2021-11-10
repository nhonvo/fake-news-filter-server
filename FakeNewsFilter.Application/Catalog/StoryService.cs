using AutoMapper;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Follows;
using FakeNewsFilter.ViewModel.Catalog.SourceStory;
using FakeNewsFilter.ViewModel.Catalog.Story;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.Application.Catalog
{
    public interface IStoryService
    {
        Task<ApiResult<int>> Create(StoryCreateRequest request);
        Task<ApiResult<bool>> Update(StoryUpdateRequest request);
        Task<ApiResult<bool>> Delete(int StoryId);
        Task<ApiResult<StoryViewModel>> GetOneStory(int StoryId);
        //Task<ApiResult<StoryViewModel>> GetAllStory(string languageId);
    }
    public class StoryService : IStoryService
    {
        private readonly ApplicationDBContext _context;
        private readonly FileStorageService _storageService;
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public StoryService(ApplicationDBContext context, FileStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
            FileStorageService.USER_CONTENT_FOLDER_NAME = "images/storys";
        }

        public async Task<ApiResult<int>> Create(StoryCreateRequest request)
        {
            //check SourceId
            var source = await _context.Source.FindAsync(request.SourceId);
            if (source == null)
            {
                return new ApiErrorResult<int>("SourceId not exist");
            }
            //add item in story
            var story = new Story()
            {
                Timestamp = DateTime.Now,
                Link = request.Link,
                SourceId = request.SourceId
            };
            //Save Image on Host
            if (request.ThumbStory != null)
            {
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
                return new ApiErrorResult<int>("Create Story Unsuccessful! Try again");
            }

            return new ApiSuccessResult<int>("Create Story Successful!", story.StoryId);

        }
        public async Task<ApiResult<bool>> Update(StoryUpdateRequest request)
        {
            //Check StoryId
            var story_update = await _context.Story.FindAsync(request.StoryId);

            if (story_update == null)
                return new ApiErrorResult<bool>($"Cannont find a story with Id is: {request.StoryId}");

            //checkSourceId
            var source_update = await _context.Story.FindAsync(request.SourceId);

            if (source_update == null)
                return new ApiErrorResult<bool>($"Cannont find a source with Id is: {request.SourceId}");

            //update link, sourceId
            story_update.Link = request.Link ?? story_update.Link;

            story_update.SourceId = request?.SourceId ?? story_update.SourceId;
            
            //Update image
            if (request.ThumbStory != null)
            {
                //Check image
                var thumb = _context.Media.FirstOrDefault(i => i.MediaId == story_update.ThumbNews);

                //If image null, create new image
                if (thumb == null)
                {
                    story_update.Media = new Media()
                    {
                        Caption = "Thumbnail story",
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
                return new ApiErrorResult<bool>("Update News Unsuccessful! Try again");
            }

            return new ApiSuccessResult<bool>("Update News Successful!", false);
        }
        public async Task<ApiResult<bool>> Delete(int StoryId)
        {
            try
            {
                var story = await _context.Story.FindAsync(StoryId);

                if (story == null) throw new FakeNewsException($"Cannot find a Story with Id: {StoryId}");

                var media = _context.Media.Single(x => x.MediaId == story.ThumbNews);

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
                    return new ApiSuccessResult<bool>("Delete Story Successful!", false);
                }

                return new ApiErrorResult<bool>("Delete Story Unsuccessful.");
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
                    return new ApiErrorResult<StoryViewModel>("Story doesn't exist!");
                }
                var storyVM = new StoryViewModel
                {
                    StoryId = story.StoryId,
                    Link = story.Link,
                    SourceId = story.SourceId,
                    ThumbNews = story.ThumbNews,
                    SyncTime = DateTime.Now - story.Timestamp
                };

                return new ApiSuccessResult<StoryViewModel>("Get Story successful!", storyVM);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<StoryViewModel>(ex.Message);
            }
        }
        //public async Task<ApiResult<StoryViewModel>> GetAllStory(string languageId)
        //{

        //}
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
