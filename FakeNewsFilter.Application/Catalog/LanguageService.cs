using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.Catalog.Language;
using FakeNewsFilter.Data.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog
{
    public interface ILanguageService
    {
        Task<ApiResult<List<GetLanguageRequest>>> GetAllLanguage();
    }

    public class LanguageService : ILanguageService
    {
        private readonly ApplicationDBContext _context;


        public LanguageService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<List<GetLanguageRequest>>> GetAllLanguage()
        {
            var languagesList = await _context.Languages.Select(x => new GetLanguageRequest()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsDefault = x.IsDefault
                }).ToListAsync();

            if (languagesList == null)
            {
                return new ApiErrorResult<List<GetLanguageRequest>>("Get All Languages Unsuccessful!");
            }

            return new ApiSuccessResult<List<GetLanguageRequest>>("Get All Languages Successful!", languagesList);
        }
    }
}
