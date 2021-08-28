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
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog.TopicNews
{
    public class TopicService : ITopicService
    {
        private readonly ApplicationDBContext _context;

        private FileStorageService _storageService;

        public TopicService(ApplicationDBContext context, FileStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        //Get 10 Topic News Hot
        public async Task<ApiResult<List<TopicInfoVM>>> GetTopicHotNews()
        {

            var query = from t in _context.TopicNews
                        select new
                        {
                            topic = t,
                            newscount = _context.NewsInTopics.Where(n => n.TopicId == t.TopicId).Count(),
                            thumb = _context.Media.Where(m => m.MediaId == t.ThumbTopic).Select(m => m.PathMedia).FirstOrDefault(),
                            synctime = _context.NewsInTopics.Where(n => n.TopicId == t.TopicId).Max(n=>n.Timestamp)
                        };

            var topics = await query.Select(x => new TopicInfoVM()
            {
                TopicId = x.topic.TopicId,
                Label = x.topic.Label,
                Tag = x.topic.Tag,
                Description = x.topic.Description,
                NONews = x.newscount,
                ThumbImage = x.thumb,
                RealTime = x.synctime,
            }).ToListAsync();

            return new ApiSuccessResult<List<TopicInfoVM>>("Get 10 Topic News Hot Successful!",topics) ;
        }

        //Create Topic News
        public async Task<ApiResult<bool>> Create(TopicNewsCreateRequest request)
        {
            var topic = new Data.Entities.TopicNews()
            {
                Label = request.Label,
                Description = request.Description,
                Tag = request.Tag,
                Timestamp = DateTime.Now
            };

            //Save Media
            if (request.ThumbnailMedia != null)
            {
                topic.Media = new Media()
                {
                    Caption = "Thumbnail Topic",
                    DateCreated = DateTime.Now,
                    FileSize = request.ThumbnailMedia.Length,
                    PathMedia = await this.SaveFile(request.ThumbnailMedia),
                    Type = MediaType.Image,
                    SortOrder = 1
                };
            }

            _context.TopicNews.Add(topic);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiSuccessResult<bool>("Create Topic Successful!", false);
            }

            return new ApiErrorResult<bool>("Create Unsuccessful.");

            
        }

        public async Task<ApiResult<bool>> Delete(int TopicId)
        {
            var topic = await _context.TopicNews.FindAsync(TopicId);

            if (topic == null) throw new FakeNewsException($"Cannot find a News with Id: {TopicId}");

            var media = _context.Media.Find(TopicId);

            if (media != null && media.PathMedia != null)
                await _storageService.DeleteFileAsync(media.PathMedia);

            _context.TopicNews.Remove(topic);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiSuccessResult<bool>("Delete Topic Successful!", false);
            }

            return new ApiErrorResult<bool>("Delete Unsuccessful.");
        }

        public async Task<ApiResult<bool>> Update(TopicNewsUpdateRequest request)
        {
            var topic = await _context.TopicNews.FindAsync(request.TopicId);

            if (topic == null)
                throw new FakeNewsException($"Cannont find a topic news with Id is: {request.TopicId}");

            topic.Label = request.Label;
            topic.Description = request.Description;
            topic.Timestamp = DateTime.Now;

            if (request.ThumbnailMedia != null)
            {
                var thumb = _context.Media.FirstOrDefault(i => i.MediaId == topic.ThumbTopic);

                if (thumb.PathMedia != null)
                {
                    await _storageService.DeleteFileAsync(thumb.PathMedia);
                }
                
                thumb.FileSize = request.ThumbnailMedia.Length;
                thumb.PathMedia = await SaveFile(request.ThumbnailMedia);

                thumb.Type = request.Type;

                _context.Media.Update(thumb);
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiSuccessResult<bool>("Update Topic Successful!", false);
            }

            return new ApiErrorResult<bool>("Update Unsuccessful.");
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