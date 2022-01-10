using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;

namespace FakeNewsFilter.Application.Common;

public class NewsCommon
{
    public static async Task<News> CheckExistNews(ApplicationDBContext _context, int newsId)
    {
        var newsUpdate = await _context.News.FindAsync(newsId);
        return newsUpdate;
    }
    
}