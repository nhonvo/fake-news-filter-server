using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;

namespace FakeNewsFilter.Application.Common
{
    public class NewscommunityCommon
    {
        public static async Task<NewsCommunity> CheckExistNews(ApplicationDBContext _context, int newsId)
        {
            var newsUpdate = await _context.NewsCommunity.FindAsync(newsId);
            return newsUpdate;
        }
    }
}
