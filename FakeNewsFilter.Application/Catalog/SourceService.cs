using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
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
        Task<ApiResult<string>> Create(SourceCreateRequest request);
        Task<ApiResult<string>> Update(SourceUpdateRequest request);
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
        public async Task<ApiResult<string>> Create(SourceCreateRequest request)
        {
            try
            {
                //Kiểm tra ngôn ngữ có trong hệ thống hay không
                var language = _context.Languages.FirstOrDefault(x => x.Id == request.LanguageId);
                if(language == null)
                {
                    return new ApiErrorResult<string>("LanguageNotFound", " " + request.LanguageId);
                }
                //Kiểm tra đã tồn tại trong hệ thống hay chưa
                var sourcename = _context.Source.FirstOrDefault(x => x.SourceName == request.SourceName);
                if(sourcename != null)
                {
                    return new ApiErrorResult<string>("SourceNameFound", " " + request.SourceName);
                }
                //tạo 1 nguồn mới
                var ScourceStory = new Source()
                {
                    SourceName = request.SourceName,
                    LanguageId = request.LanguageId
                };

                _context.Source.Add(ScourceStory);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    return new ApiErrorResult<string>("CreateSourceStoryUnsuccessful", result.ToString());
                }

                return new ApiSuccessResult<string>("CreateSourceStorySuccessful", sourcename.SourceId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Cập nhật nguồn
        public async Task<ApiResult<string>> Update(SourceUpdateRequest request)
        {
            try
            {
                //Kiểm tra nguồn có tồn tại trong hệ thống hay không
                var sourcename = _context.Source.FirstOrDefault(x => x.SourceName == request.SourceName);
                if (sourcename == null)
                {
                    return new ApiErrorResult<string>("CannotFindSourceNameExist", " " + request.SourceName);
                }
                //Kiểm tra ngôn ngữ có tồn tại trong hệ thống hay không
                var language = _context.Languages.FirstOrDefault(x => x.Id == request.LanguageId);
                if (language == null)
                {
                    return new ApiErrorResult<string>("LanguageNotFound", " " + request.LanguageId);
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

                if (result > 0)
                {
                    return new ApiSuccessResult<string>("SourceStoryUpdateSuccessful", " " + sourcename.SourceId.ToString());
                }

                return new ApiErrorResult<string>("SourceStoryUpdateUnsuccessful"," " + result.ToString());
            }
            catch (DbUpdateException ex)
            {
                return new ApiErrorResult<string>(ex.Message);
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
                return new ApiErrorResult<List<SourceViewModel>>("GetAllSourceStoryUnsuccessful");
            }

            return new ApiSuccessResult<List<SourceViewModel>>("GetAllSourceStorySuccessful", list_source);
        }
        //Lấy 1 nguồn
        public async Task<ApiResult<SourceViewModel>> GetoneStory(int sourceid)
        {
            var sourcestory = await _context.Source.FirstOrDefaultAsync(t => t.SourceId == sourceid);
            if(sourcestory == null)
            {
                return new ApiErrorResult<SourceViewModel>("SourceNotFound");
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
                return new ApiErrorResult<SourceViewModel>("GetOneSourceStoryUnsuccessful");
            }
        }
        //Xóa nguồn
        public async Task<ApiResult<string>> Delete(int sourceid)
        {
            var deletesourceId = _context.Source.FirstOrDefault(x => x.SourceId == sourceid);
            
            if (deletesourceId == null)
            {
                return new ApiErrorResult<string>("CanNotFindSourceid", sourceid);
            }
            
            _context.Source.Remove(deletesourceId);

            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<string>("DeleteSourceStoryUnsuccessful"," " + result.ToString());
            }

            return new ApiSuccessResult<string>("DeleteSourceStorySuccessful", " " + deletesourceId.SourceId.ToString());

        }

    }


}
