using AutoMapper;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Follows;
using FakeNewsFilter.ViewModel.Catalog.SourceStory;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    return new ApiErrorResult<bool>("Language not exist");
                }
                //check SourceName
                var sourcename = _context.Source.FirstOrDefault(x => x.SourceName == request.SourceName);
                if(sourcename != null)
                {
                    return new ApiErrorResult<bool>($"Source Name {request.SourceName} exist");
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
                    return new ApiErrorResult<bool>("Create Source Story Unsuccessful! Try again");
                }

                return new ApiSuccessResult<bool>("Create Source Story Successful!", false);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //update source story
        public async Task<ApiResult<bool>> Update(SourceUpdateRequest request)
        {
            //check Language
            var language = _context.Languages.FirstOrDefault(x => x.Id == request.LanguageId);
            if (language == null)
            {
                return new ApiErrorResult<bool>("Language not exist");
            }
            //check SourceName
            var sourcename = _context.Source.FirstOrDefault(x => x.SourceName == request.SourceName);
            if (sourcename == null)
            {
                return new ApiErrorResult<bool>($"Can not find Source Name {request.SourceName} exist");
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
                return new ApiSuccessResult<bool>("SourceStory update Successful!", false);
            }

            return new ApiErrorResult<bool>("SourceStory update Unsuccessful.");
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
                return new ApiErrorResult<List<SourceViewModel>>("Get All Source Story Unsuccessful!");
            }

            return new ApiSuccessResult<List<SourceViewModel>>("Get All Source Story Successful!", list_source);
        }
        //get one story in SourceId
        public async Task<ApiResult<SourceViewModel>> GetoneStory(int sourceid)
        {
            //get sourceId
            var sourcestory = await _context.Source.FirstOrDefaultAsync(t => t.SourceId == sourceid);
            SourceViewModel result = null;
            if (sourcestory != null)
            {
                result = new SourceViewModel()
                {
                    SourceId = sourcestory.SourceId,
                    SourceName = sourcestory.SourceName,
                    LanguageId = sourcestory.LanguageId
                };

                return new ApiSuccessResult<SourceViewModel>("Get One Source Story Successful!", result);
            }
            else
            {
                return new ApiErrorResult<SourceViewModel>("Get One Source Story unsuccessful!");
            }
        }
        //Delete source story
        public async Task<ApiResult<bool>> Delete(int sourceid)
        {
            var deletesourceId = _context.Source.FirstOrDefault(x => x.SourceId == sourceid);
            
            if (deletesourceId == null)
            {
                return new ApiErrorResult<bool>("can not find sourceid");
            }
            
            _context.Source.Remove(deletesourceId);

            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("Delete Source Story Unsuccessful! Try again");
            }

            return new ApiSuccessResult<bool>("Delete Source Story Successful!", false);

        }

    }


}
