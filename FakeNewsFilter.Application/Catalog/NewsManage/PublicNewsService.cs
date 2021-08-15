using System;
using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.Catalog.News;
using FakeNewsFilter.ViewModel.Catalog.News.Public;

namespace FakeNewsFilter.Application.Catalog.NewsManage
{
    public class PublicNewsService : IPublicNewsService
    {
        private readonly ApplicationDBContext _context;

        public PublicNewsService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<NewsViewModel>> GetAllByTopicId(GetNewsPagingRequest request)
        {
            //1. Select Join
            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            //2. Filter
            if (request.TopicId.HasValue && request.TopicId.Value > 0)
            {
                query = query.Where(t => t.nit.TopicId == request.TopicId);
            }

            //3. Paging
            int TotalRow = await query.CountAsync();

            var data = await query
                .Skip((request.pageIndex - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(x => new NewsViewModel()
                {
                    NewsId = x.n.NewsId,
                    Name = x.n.Name,
                    TopicId = x.c.TopicId,
                    TopicName = x.c.TopicName,

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<NewsViewModel>()
            {
                TotalRecord = TotalRow,
                Items = data
            };

            return pagedResult;
        }

        
    }
}
