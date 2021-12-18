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
        Task<ApiResult<bool>> Create(SourceCreateRequest request);
        Task<ApiResult<bool>> Update(SourceUpdateRequest request);
        Task<ApiResult<bool>> Delete(int sourceid);
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
        //create source story
        public async Task<ApiResult<bool>> Create(SourceCreateRequest request)
        {
            try
            {
                //check Language
                var language = _context.Languages.FirstOrDefault(x => x.Id == request.LanguageId);
                if(language == null)
                {
                    return new ApiErrorResult<bool>("LanguageNotFound");
                }
                //check SourceName
                var sourcename = _context.Source.FirstOrDefault(x => x.SourceName == request.SourceName);
                if(sourcename != null)
                {
                    return new ApiErrorResult<bool>("SourceNameFound");
                }
                //create new scourcename
                var ScourceStory = new Source()
                {
                    SourceName = request.SourceName,
                    LanguageId = request.LanguageId
                };

                _context.Source.Add(ScourceStory);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    return new ApiErrorResult<bool>("CreateSourceStoryUnsuccessful");
                }

                return new ApiSuccessResult<bool>("CreateSourceStorySuccessful", false);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //update source story
        public async Task<ApiResult<bool>> Update(SourceUpdateRequest request)
        {
            try
            {
                //check SourceName
                var sourcename = _context.Source.FirstOrDefault(x => x.SourceName == request.SourceName);
                if (sourcename == null)
                {
                    return new ApiErrorResult<bool>("CannotFindSourceNameExist");
                }
                //check Language
                var language = _context.Languages.FirstOrDefault(x => x.Id == request.LanguageId);
                if (language == null)
                {
                    return new ApiErrorResult<bool>("LanguageNotFound");
                }

                //remove source name
                var removelanguageid = _context.Source.Where(t => t.LanguageId == request.LanguageId);
                _context.Source.RemoveRange(removelanguageid);
                await _context.SaveChangesAsync();

                //update source name
                var sourceUpdate = new Data.Entities.Source()
                {
                    SourceName = request.SourceName,
                    LanguageId = request.LanguageId
                };
                _context.Source.Add(sourceUpdate);

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<bool>("SourceStoryUpdateSuccessful", false);
                }

                return new ApiErrorResult<bool>("SourceStoryUpdateUnsuccessful");
            }
            catch (DbUpdateException ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }

        }
        //get all source story
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
        //get one story in SourceId
        public async Task<ApiResult<SourceViewModel>> GetoneStory(int sourceid)
        {
            //get sourceId
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
        //Delete source story
        public async Task<ApiResult<bool>> Delete(int sourceid)
        {
            var deletesourceId = _context.Source.FirstOrDefault(x => x.SourceId == sourceid);
            
            if (deletesourceId == null)
            {
                return new ApiErrorResult<bool>("CanNotFindSourceid");
            }
            
            _context.Source.Remove(deletesourceId);

            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("DeleteSourceStoryUnsuccessful");
            }

            return new ApiSuccessResult<bool>("DeleteSourceStorySuccessful", false);

        }

    }


}
