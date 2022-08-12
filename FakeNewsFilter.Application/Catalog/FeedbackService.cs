using System;
using FakeNewsFilter.ViewModel.Catalog.Vote;
using FakeNewsFilter.ViewModel.Common;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.Feedback;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.Entities;
using System.IO;
using System.Net;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace FakeNewsFilter.Application.Catalog
{
    public interface IFeedbackService
    {
        Task<ApiResult<string>> Create(CreateFeedbackRequest request);

        Task<ApiResult<string>> ReportLinkNews(CreateFeedbackRequest request);

        Task<ApiResult<bool>> ChangeStatusFeedback();
    }

    public class FeedbackService : IFeedbackService
    {

        private readonly ApplicationDBContext _context;

        private readonly FileStorageService _storageService;

        public static readonly List<string> ImageExtensions = new() { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG", ".JPEG" };

        public FeedbackService(ApplicationDBContext context, FileStorageService storageService)
        {
            _context = context;
            FileStorageService.USER_CONTENT_FOLDER_NAME = "images/feedback";
            _storageService = storageService;
        }


        public async Task<ApiResult<string>> Create(CreateFeedbackRequest request)
        {
            try
            {
                if(request.NewsId != null)
                {
                    var news = await NewsCommon.CheckExistNews(_context, (int)request.NewsId);

                    if (news == null) return new ApiErrorResult<string>(404, "NewsIsNotExist", " " + request.NewsId);

                }
                if (request.UserId != null)
                {
                    var user = await UserCommon.CheckExistUser(_context, (Guid)request.UserId);

                    if (user == null) return new ApiErrorResult<string>(404, "UserIsNotExist", " " + request.UserId.ToString());

                }
                var feedback = new Feedback
                {
                    UserId = request.UserId,
                    NewsId = request.NewsId,
                    Content = request.Content,
                    Status = Data.Enums.Status.Pending,
                    Timestamp = DateTime.Now

                };

                if (request.ScreenShoot != null) {
                    
                    var checkExtension =
                        ImageExtensions.Contains(Path.GetExtension(request.ScreenShoot.FileName)
                            .ToUpperInvariant());

                    if (checkExtension == false)
                    {
                        return new ApiErrorResult<string>(400, "FileImageInvalid");
                    }

                    feedback.Media = new Media
                    {
                        Caption = "Screenshoot Feedback",
                        DateCreated = DateTime.Now,
                        FileSize = request.ScreenShoot.Length,
                        PathMedia = await SaveFile(request.ScreenShoot),
                        Type = MediaType.Image,
                        SortOrder = 1
                    };
                }
                
                _context.Feedback.Add(feedback);

                var result = await _context.SaveChangesAsync();

                if (result != 0) return new ApiSuccessResult<string>("FeedbackSuccessful");

                return new ApiErrorResult<string>(400, "FeedbackUnSuccessful", " " + result.ToString());

            }
            catch(Exception ex)
            {
                return new ApiErrorResult<string>(500, ex.Message);
            }
        }


        public async Task<ApiResult<string>> ReportLinkNews(CreateFeedbackRequest request)
        {
            try
            {
                if (request.UserId != null)
                {
                    var user = await UserCommon.CheckExistUser(_context, (Guid)request.UserId);

                    if (user == null) return new ApiErrorResult<string>(404, "UserIsNotExist");

                }

                if (request.NewsId == null)
                {
                    return new ApiErrorResult<string>(404, "NewsIdIsRequire");
                }

                var news = await NewsCommon.CheckExistNews(_context, (int)request.NewsId);

                if (news == null) return new ApiErrorResult<string>(404, "NewsIsNotExist", " " + request.NewsId);

                var feedback = new Feedback
                {
                    UserId = request.UserId,
                    NewsId = request.NewsId,
                    Content = request.Content,
                    Status = Data.Enums.Status.Pending,
                    Timestamp = DateTime.Now

                };
                _context.Feedback.Add(feedback);

                var result = await _context.SaveChangesAsync();

                if (result != 0) return new ApiSuccessResult<string>("ReportLinkSuccessful");

                return new ApiErrorResult<string>(400, "ReportLinkUnSuccessful", " " + result.ToString());

            }
            catch(Exception ex)
            {
                return new ApiErrorResult<string>(500, ex.Message);
            }
        }

        public Task<ApiResult<bool>> ChangeStatusFeedback()
        {
            throw new NotImplementedException();
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

