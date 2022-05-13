using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Common;

public class NewsCommon
{
    public static async Task<News> CheckExistNews(ApplicationDBContext _context, int newsId)
    {
        var newsUpdate = await _context.News
            .Include(i => i.DetailNews)
            .Include(i=>i.NewsInTopics)
            .FirstOrDefaultAsync(x=>x.NewsId == newsId);
        return newsUpdate;
    }
    
}