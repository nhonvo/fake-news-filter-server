using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;

namespace FakeNewsFilter.Application.Common
{
    public class LanguageCommon
    {
        public static async Task<Language> CheckExistLanguage(ApplicationDBContext _context, string language)
        {
            var languageUpdate = await _context.Languages.FindAsync(language);
            return languageUpdate;
        }
    }
}
