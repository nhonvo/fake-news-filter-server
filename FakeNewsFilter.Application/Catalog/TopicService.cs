using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog
{
    public interface ITopicService
    {
        Task<ApiResult<List<TopicInfoVM>>> GetTopicHotNews(string languageId);

        Task<ApiResult<string>> Create(TopicCreateRequest request);

        Task<ApiResult<TopicInfoVM>> GetTopicById(int Id);

        Task<ApiResult<string>> Delete(int TopicId);

        Task<ApiResult<string>> Update(TopicUpdateRequest request);
    }

    public class TopicService : ITopicService
    {
        private readonly ApplicationDBContext _context;

        private FileStorageService _storageService;

        public TopicService(ApplicationDBContext context, FileStorageService storageService)
        {
            _context = context;
            FileStorageService.USER_CONTENT_FOLDER_NAME = "images/topics";
            _storageService = storageService;
        }

        //Lấy 10 chủ đề tin tức nóng nhất
        public async Task<ApiResult<List<TopicInfoVM>>> GetTopicHotNews(string languageId)
        {
            
                try
                {
                    var language = await _context.Languages.SingleOrDefaultAsync(x => x.Id == languageId);

                    var query = from t in _context.TopicNews
                                where (string.IsNullOrEmpty(languageId) || t.LanguageId == languageId)
                                select new
                                {
                                    topic = t,
                                    newscount = _context.NewsInTopics.Where(n => n.TopicId == t.TopicId).Count(),
                                    thumb = _context.Media.Where(m => m.MediaId == t.ThumbTopic).Select(m => m.PathMedia).FirstOrDefault(),
                                    synctime = _context.NewsInTopics.Where(n => n.TopicId == t.TopicId).Max(n => n.Timestamp)
                                };

                    var topics = await query.Select(x => new TopicInfoVM()
                    {
                        TopicId = x.topic.TopicId,
                        Label = x.topic.Label,
                        Tag = x.topic.Tag,
                        Description = x.topic.Description,
                        NONews = x.newscount,
                        ThumbImage = x.thumb,
                        Status = x.topic.Status,
                        LanguageId = x.topic.LanguageId,
                        RealTime = x.synctime,
                    }).ToListAsync();

                    if (topics == null)
                    {
                        return new ApiErrorResult<List<TopicInfoVM>>("GetTopicUnsuccessful", topics);
                    }

                    if (language == null)
                    {
                        return new ApiSuccessResult<List<TopicInfoVM>>("GetTopicSuccessful", topics);
                    }

                    return new ApiSuccessResult<List<TopicInfoVM>>("GetTopicSuccessful", topics);
                }
                catch (Exception ex)
                {
                    return new ApiErrorResult<List<TopicInfoVM>>(ex.Message);
                }
            
            
        }

        //Tạo chủ đề mới
        public async Task<ApiResult<string>> Create(TopicCreateRequest request)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var language = await _context.Languages.FirstOrDefaultAsync(x => x.Id == request.LanguageId);
                    if (language == null)
                    {
                        return new ApiErrorResult<string>("LanguageNotFound", " " + request.LanguageId);
                    }
                    var topic = new Data.Entities.TopicNews()
                    {
                        Label = request.Label,
                        Description = request.Description,
                        Tag = request.Tag,
                        Timestamp = DateTime.Now,
                        LanguageId = request.LanguageId
                    };

                    //Lưu ảnh, video,...
                    if (request.ThumbTopic != null)
                    {
                        topic.Media = new Media()
                        {
                            Caption = "Thumbnail Topic",
                            DateCreated = DateTime.Now,
                            FileSize = request.ThumbTopic.Length,
                            PathMedia = await this.SaveFile(request.ThumbTopic),
                            Type = MediaType.Image,
                            SortOrder = 1
                        };
                    }

                    _context.TopicNews.Add(topic);

                    var result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        transaction.Commit();
                        return new ApiSuccessResult<string>("CreateTopicSuccessful", " " + topic.TopicId.ToString());
                    }

                    transaction.Rollback();
                    return new ApiErrorResult<string>("CreateUnsuccessful", " " + result.ToString());
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<string>(ex.Message);
                }
            }

        }

        //Cập nhật Topic
        public async Task<ApiResult<string>> Update(TopicUpdateRequest request)
        {
            try
            {
                var topic = await _context.TopicNews.FindAsync(request.TopicId);
                if (topic == null)
                {
                    return new ApiErrorResult<string>("TopicNotFound", " " + request.TopicId.ToString());
                }

                var language = await _context.Languages.FirstOrDefaultAsync(x => x.Id == request.LanguageId);
                if(language == null)
                {
                    return new ApiErrorResult<string>("LanguageNotFound", " " + request.LanguageId);
                }
                
                topic.Label = request.Label ?? topic.Label;
                topic.Tag = request.Tag ?? topic.Tag;
                topic.Description = request.Description ?? topic.Description;
                topic.Timestamp = DateTime.Now;
                topic.LanguageId = request.LanguageId;

                if (request.ThumbImage != null)
                {
                    //Kiểm tra hình đã có trên DB chưa
                    var thumb = _context.Media.FirstOrDefault(i => i.MediaId == topic.ThumbTopic);

                    //Nếu chưa có hình thì thêm hình mới
                    if (thumb == null)
                    {
                        topic.Media = new Media()
                        {
                            Caption = "Thumbnail Topic",
                            DateCreated = DateTime.Now,
                            FileSize = request.ThumbImage.Length,
                            PathMedia = await this.SaveFile(request.ThumbImage),
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
                        thumb.FileSize = request.ThumbImage.Length;
                        thumb.PathMedia = await SaveFile(request.ThumbImage);

                        thumb.Type = MediaType.Image;

                        _context.Media.Update(thumb);
                    }
                }

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<string>("UpdateTopicSuccessful", " " + topic.TopicId.ToString());
                }

                return new ApiErrorResult<string>("UpdateUnsuccessful", " " + result.ToString());
            }
            catch(Exception ex)
            {
                return new ApiErrorResult<string>(ex.Message);
            }
            
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        //Lấy thông tin chi tiết 1 Topic
        public async Task<ApiResult<TopicInfoVM>> GetTopicById(int id)
        {
            try
            {
                var topic = await _context.TopicNews.FindAsync(id);

                var thumb = _context.Media.Where(m => m.MediaId == topic.ThumbTopic).Select(m => m.PathMedia).FirstOrDefault();

                if (topic == null)
                {
                    return new ApiErrorResult<TopicInfoVM>("TopicNotFound", id);
                }
                var topicvm = new TopicInfoVM
                {
                    TopicId = topic.TopicId,
                    Tag = topic.Tag,
                    ThumbImage = thumb,
                    Description = topic.Description,
                    Label = topic.Label,
                    Status = topic.Status,
                    LanguageId = topic.LanguageId,
                    RealTime = DateTime.Now
                };

                return new ApiSuccessResult<TopicInfoVM>("GetInfoTopicSuccessful", topicvm);
            }
            catch(Exception ex)
            {
                return new ApiErrorResult<TopicInfoVM>(ex.Message);
            }
            
        }

        //Xoá Topic
        public async Task<ApiResult<string>> Delete(int TopicId)
        {
            try
            {
                var topic = await _context.TopicNews.FindAsync(TopicId);

                if (topic == null) return new ApiSuccessResult<string>($"CannotFindTopicWithId"," " + TopicId.ToString());

                if(topic.ThumbTopic != null)
                {
                    var media = _context.Media.SingleOrDefault(x => x.MediaId == topic.ThumbTopic);

                    if (media != null)
                    {
                        if (media.PathMedia != null)
                            await _storageService.DeleteFileAsync(media.PathMedia);
                        _context.Media.Remove(media);
                    }
                }

                _context.TopicNews.Remove(topic);

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<string>("DeleteTopicSuccessful", " " + topic.TopicId.ToString());
                }

                return new ApiErrorResult<string>("DeleteUnsuccessful", " " + result);
            }

            catch(Exception ex)
            {
                return new ApiErrorResult<string>(ex.Message);
            }
        }

    }
}