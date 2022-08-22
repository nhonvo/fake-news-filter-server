using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.ViewModel.Catalog.SourceStory;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeNewsFilter.Application.Catalog
{
    public interface IScourceService
    {
        Task<ApiResult<SourceViewModel>> Create(SourceCreateRequest request);
        Task<ApiResult<SourceViewModel>> Update(SourceUpdateRequest request);
        Task<ApiResult<SourceViewModel>> Archive(SourceUpdateRequest request);
        Task<ApiResult<string>> Delete(int sourceid);
        Task<ApiResult<List<SourceViewModel>>> GetAll(string languageId);
        Task<ApiResult<SourceViewModel>> GetoneStory(int SourceId);


    }
    public class SourceService : IScourceService
    {
        private readonly ApplicationDBContext _context;
        public SourceService(ApplicationDBContext context)
        {
            _context = context;
        }
        //Tạo nguồn mới
        public async Task<ApiResult<SourceViewModel>> Create(SourceCreateRequest request)
        {
            try
            {
                //Kiểm tra ngôn ngữ có trong hệ thống hay không
                if(request.LanguageId != null)
                {
                    var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);
                    if (language == null)
                    {
                        return new ApiErrorResult<SourceViewModel>(404, "LanguageNotFound");
                    }
                }
               
                //Kiểm tra đã tồn tại trong hệ thống hay chưa
                var sourcename = await _context.Source.FirstOrDefaultAsync(x => x.SourceName == request.SourceName);
                if (sourcename != null)
                {
                    return new ApiErrorResult<SourceViewModel>(404, "SourceNameFound");
                }

                //Tạo 1 nguồn mới
                var scourceStory = new Source()
                {
                    SourceName = request.SourceName,
                    LanguageId = request.LanguageId
                };

                _context.Source.Add(scourceStory);

                var result = await _context.SaveChangesAsync();

                var sourceModel = await GetoneStory(scourceStory.SourceId);
                if (result == 0)
                {
                    return new ApiErrorResult<SourceViewModel>(400, "CreateSourceStoryUnsuccessful", sourceModel.ResultObj);
                }

                return new ApiSuccessResult<SourceViewModel>("CreateSourceStorySuccessful", sourceModel.ResultObj);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Cập nhật nguồn
        public async Task<ApiResult<SourceViewModel>> Update(SourceUpdateRequest request)
        {
            try
            {
                //Kiểm tra nguồn có tồn tại trong hệ thống hay không
                var sourcename = SourceCommon.CheckExistSourceName(_context, request.SourceName);
                if (sourcename == null)
                {
                    return new ApiErrorResult<SourceViewModel>(404, "CannotFindSourceNameExist");
                }
                //Kiểm tra ngôn ngữ có tồn tại trong hệ thống hay không
                var language = await LanguageCommon.CheckExistLanguage(_context, request.LanguageId);
                if (language == null)
                {
                    return new ApiErrorResult<SourceViewModel>(404, "LanguageNotFound");
                }

                //Xóa nguồn cũ
                var removelanguageid = _context.Source.Where(t => t.LanguageId == request.LanguageId);
                _context.Source.RemoveRange(removelanguageid);
                await _context.SaveChangesAsync();

                //Cập nhật nguồn
                var sourceUpdate = new Data.Entities.Source()
                {
                    SourceName = request.SourceName,
                    LanguageId = request.LanguageId
                };
                _context.Source.Add(sourceUpdate);

                var result = await _context.SaveChangesAsync();
                var sourceModel = await GetoneStory(sourceUpdate.SourceId);
                if (result > 0)
                {
                    return new ApiSuccessResult<SourceViewModel>("SourceStoryUpdateSuccessful", sourceModel.ResultObj);
                }

                return new ApiErrorResult<SourceViewModel>(400, "SourceStoryUpdateUnsuccessful", sourceModel.ResultObj);
            }
            catch (DbUpdateException ex)
            {
                return new ApiErrorResult<SourceViewModel>(500, ex.Message);
            }

        }
        //Lấy tất cả các nguồn
        public async Task<ApiResult<List<SourceViewModel>>> GetAll(string languageId)
        {
            var list_source = await _context.Source.Where(n => !string.IsNullOrEmpty(languageId) ? n.LanguageId == languageId : true)
                .Select(x => new SourceViewModel()
                {
                    SourceId = x.SourceId,
                    SourceName = x.SourceName,
                    LanguageId = x.LanguageId
                }).ToListAsync();

            if (list_source == null)
            {
                return new ApiErrorResult<List<SourceViewModel>>(400, "GetAllSourceStoryUnsuccessful");
            }

            return new ApiSuccessResult<List<SourceViewModel>>("GetAllSourceStorySuccessful", list_source);
        }
        //Lấy 1 nguồn
        public async Task<ApiResult<SourceViewModel>> GetoneStory(int sourceid)
        {
            var sourcestory = await SourceCommon.CheckExistSource(_context, sourceid);
            if (sourcestory == null)
            {
                return new ApiErrorResult<SourceViewModel>(404, "SourceNotFound");
            }
            SourceViewModel result = null;
            if (sourcestory != null)
            {
                result = new SourceViewModel()
                {
                    SourceId = sourcestory.SourceId,
                    SourceName = sourcestory.SourceName,
                    LanguageId = sourcestory.LanguageId
                };

                return new ApiSuccessResult<SourceViewModel>("GetOneSourceStorySuccessful", result);
            }
            else
            {
                return new ApiErrorResult<SourceViewModel>(400, "GetOneSourceStoryUnsuccessful");
            }
        }
        //Xóa nguồn
        public async Task<ApiResult<string>> Delete(int sourceid)
        {
            var deletesourceId = await SourceCommon.CheckExistSource(_context, sourceid);

            if (deletesourceId == null)
            {
                return new ApiErrorResult<string>(404, "CanNotFindSourceid", " " + sourceid.ToString());
            }
            
            _context.Source.Remove(deletesourceId);

            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<string>(400, "DeleteSourceStoryUnsuccessful"," " + result.ToString());
            }

            return new ApiSuccessResult<string>("DeleteSourceStorySuccessful", " " + deletesourceId.SourceId.ToString());

        }

        public async Task<ApiResult<SourceViewModel>> Archive(SourceUpdateRequest request)
        {
            var source = await SourceCommon.CheckExistSource(_context, request.SourceId);

            if (source == null)
                return new ApiErrorResult<SourceViewModel>(404, "CannontFindCommentWithId");

            source.Status = Status.Archive;

            var result = await _context.SaveChangesAsync();
            var sourceModel = await GetoneStory(source.SourceId);
            if (result == 0) return new ApiErrorResult<SourceViewModel>(400, "UpdateLinkNewsUnsuccessful", sourceModel.ResultObj);

            return new ApiSuccessResult<SourceViewModel>("UpdateLinkNewsSuccessful", sourceModel.ResultObj);
        }
    }


}
