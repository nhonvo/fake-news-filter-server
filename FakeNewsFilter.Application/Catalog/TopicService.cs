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
        Task<ApiResult<List<TopicInfoVM>>> GetAllTopic(string languageId);

        Task<ApiResult<PagedResult<TopicInfoVM>>> GetTopicPaging(GetTopicNewsRequest request);

        Task<ApiResult<TopicInfoVM>> Create(TopicCreateRequest request);

        Task<ApiResult<TopicInfoVM>> GetTopicById(int Id);

        Task<ApiResult<String>> Delete(int TopicId);

        Task<ApiResult<TopicInfoVM>> Update(TopicUpdateRequest request);
        Task<ApiResult<TopicInfoVM>> Archive(TopicUpdateRequest request);
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

        //Lấy toàn bộ tin tức
        public async Task<ApiResult<List<TopicInfoVM>>> GetAllTopic(string LanguageId)
        {
            try
            {
                //1. Chạy câu truy vấn
                if(LanguageId != null)
                {
                    var language = await LanguageCommon.CheckExistLanguage(_context, LanguageId);

                    if (language == null)
                        return new ApiErrorResult<List<TopicInfoVM>>(404, "LanguageNotFound");

                }

                var query = from t in _context.TopicNews
                            where (string.IsNullOrEmpty(LanguageId) || t.LanguageId == LanguageId)
                            select new
                            {
                                topic = t,
                                newscount = _context.NewsInTopics.Where(n => n.TopicId == t.TopicId).Count(),
                                thumb = _context.Media.Where(m => m.MediaId == t.ThumbTopic).Select(m => m.PathMedia).FirstOrDefault(),
                                synctime = _context.NewsInTopics.Where(n => n.TopicId == t.TopicId).Max(n => n.Timestamp)
                            };

                var data = await query
                    .Select(x => new TopicInfoVM()
                    {
                        TopicId = x.topic.TopicId,
                        Label = x.topic.Label,
                        Tag = x.topic.Tag,
                        Description = x.topic.Description,
                        NONews = x.newscount,
                        ThumbImage = _storageService.GetFileUrl(x.thumb),
                        Status = x.topic.Status,
                        LanguageId = x.topic.LanguageId,
                        RealTime = x.synctime,
                    }).ToListAsync();


                if (data == null)
                {
                    return new ApiErrorResult<List<TopicInfoVM>>(400, "GetTopicUnsuccessful", data);
                }

                return new ApiSuccessResult<List<TopicInfoVM>>("GetTopicSuccessful", data);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<List<TopicInfoVM>>(500, ex.Message);
            }
        }

        //Lấy thông tin các chủ đề tin tức có phân trang
        public async Task<ApiResult<PagedResult<TopicInfoVM>>> GetTopicPaging(GetTopicNewsRequest request)
        {

            try
            {
                //1. Chạy câu truy vấn 
                if (request.LanguageId != null) { 
                    var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);
                    if (language == null)
                    {
                        return new ApiErrorResult<PagedResult<TopicInfoVM>>(404, "LanguageNotFound");
                    }
                }
                var query = from t in _context.TopicNews
                                where (string.IsNullOrEmpty(request.LanguageId) || t.LanguageId == request.LanguageId)
                                select new
                                {
                                    topic = t,
                                    newscount = _context.NewsInTopics.Where(n => n.TopicId == t.TopicId).Count(),
                                    thumb = _context.Media.Where(m => m.MediaId == t.ThumbTopic).Select(m => m.PathMedia).FirstOrDefault(),
                                    synctime = _context.NewsInTopics.Where(n => n.TopicId == t.TopicId).Max(n => n.Timestamp)
                                };

                //2. Lọc theo điều kiện
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => (x.topic.Label.Contains(request.Keyword)) ||
                                             (x.topic.Description.Contains(request.Keyword)) ||
                                             (x.topic.Status.Equals(request.Keyword))
                                       );
                }
                    

                //3. Phân trang
                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new TopicInfoVM()
                    {
                        TopicId = x.topic.TopicId,
                        Label = x.topic.Label,
                        Tag = x.topic.Tag,
                        Description = x.topic.Description,
                        NONews = x.newscount,
                        ThumbImage = _storageService.GetFileUrl(x.thumb),
                        Status = x.topic.Status,
                        LanguageId = x.topic.LanguageId,
                        RealTime = x.synctime,
                    }).ToListAsync();

                //4. Hiển thị kết quả
                var pagedResult = new PagedResult<TopicInfoVM>()
                {
                    TotalRecords = totalRow,
                    PageSize = request.PageSize,
                    PageIndex = request.PageIndex,
                    Items = data
                };

                if (pagedResult == null)
                {
                    return new ApiErrorResult<PagedResult<TopicInfoVM>>(400, "GetTopicUnsuccessful", pagedResult);
                }

                return new ApiSuccessResult<PagedResult<TopicInfoVM>>("GetTopicSuccessful", pagedResult);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<PagedResult<TopicInfoVM>>(500, ex.Message);
            } 
        }

        //Tạo chủ đề mới
        public async Task<ApiResult<TopicInfoVM>> Create(TopicCreateRequest request)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if(request.LanguageId == null)
                    {
                        return new ApiErrorResult<TopicInfoVM>(404, "PleaseEnterThisField");
                    } else
                    {
                        var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);

                        if (language == null)
                        {
                            return new ApiErrorResult<TopicInfoVM>(404, "LanguageNotFound");
                        }
                    }
                    
                    var topic = new TopicNews()
                    {
                        Label = request.Label,
                        Description = request.Description,
                        Tag = request.Tag,
                        Timestamp = DateTime.Now,
                        LanguageId = request.LanguageId,
                        Status = Status.Active
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

                    var getTopic = await GetTopicById(topic.TopicId);
                    if (result > 0)
                    {
                        transaction.Commit();
                        return new ApiSuccessResult<TopicInfoVM>("CreateTopicSuccessful", getTopic.ResultObj);
                    }

                    transaction.Rollback();
                    return new ApiErrorResult<TopicInfoVM>(400, "CreateUnsuccessful");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<TopicInfoVM>(500, ex.Message);
                }
            }

        }

        //Cập nhật Topic
        public async Task<ApiResult<TopicInfoVM>> Update(TopicUpdateRequest request)
        {
            try
            {
                var topic = await TopicCommon.CheckExistTopic(_context, request.TopicId);
                if (topic == null)
                {
                    return new ApiErrorResult<TopicInfoVM>(404, "TopicNotFound");
                }

                var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);
                if (language == null)
                {
                    return new ApiErrorResult<TopicInfoVM>(404, "LanguageNotFound");
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

                var updateModel = await GetTopicById(topic.TopicId);
                if (result > 0)
                {
                    return new ApiSuccessResult<TopicInfoVM>("UpdateTopicSuccessful", updateModel.ResultObj);
                }

                return new ApiErrorResult<TopicInfoVM>(400, "UpdateUnsuccessful");
            }
            catch(Exception ex)
            {
                return new ApiErrorResult<TopicInfoVM>(500, ex.Message);
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
                var topic = await TopicCommon.CheckExistTopic(_context, id);
                if (topic == null)
                {
                    return new ApiErrorResult<TopicInfoVM>(404, "TopicNotFound");
                }
                var thumb = _context.Media.Where(m => m.MediaId == topic.ThumbTopic).Select(m => m.PathMedia).FirstOrDefault();

                var topicvm = new TopicInfoVM
                {
                    TopicId = topic.TopicId,
                    Tag = topic.Tag,
                    ThumbImage = _storageService.GetFileUrl(thumb),
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
                return new ApiErrorResult<TopicInfoVM>(500, ex.Message);
            }
            
        }

        //Xoá Topic
        public async Task<ApiResult<String>> Delete(int topicId)
        {
            try
            {
                var topic = await TopicCommon.CheckExistTopic(_context, topicId);

                if (topic == null) return new ApiSuccessResult<String>($"CannotFindTopicWithId"," " + topicId.ToString());

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
                    return new ApiSuccessResult<String>("DeleteTopicSuccessful", " " + topic.TopicId.ToString());
                }

                return new ApiErrorResult<String>(400, "DeleteUnsuccessful", " " + result);
            }

            catch(Exception ex)
            {
                return new ApiErrorResult<String>(500, ex.Message);
            }
        }

        public async Task<ApiResult<TopicInfoVM>> Archive(TopicUpdateRequest request)
        {
            try
            {
                var topic = await TopicCommon.CheckExistTopic(_context, request.TopicId);

                if (topic == null)
                    return new ApiErrorResult<TopicInfoVM>(404, "CannontFindTopicWithId");

                topic.Status = Status.Archive;

                var result = await _context.SaveChangesAsync();

                var topicModel = await GetTopicById(topic.TopicId);
                if (result == 0)
                    return new ApiErrorResult<TopicInfoVM>(400, "ArchiveTopicUnsuccessful", topicModel.ResultObj);

                return new ApiSuccessResult<TopicInfoVM>("ArchiveTopicSuccessful");
            }
            catch(Exception ex)
            {
                return new ApiErrorResult<TopicInfoVM>(500, ex.Message);
            }
            
        }

    }
}