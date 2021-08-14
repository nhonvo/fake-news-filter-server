using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog.News.DTO.Manage;
using FakeNewsFilter.Application.DTOs;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Utilities.Exceptions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog.News.DTO
{
    public class ManageNewsService : IManageNewsService
    {
        private readonly ApplicationDBContext _context;

        public ManageNewsService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int> Create(NewsCreateRequest request)
        {
            var news = new Data.Entities.News()
            {
                NewsId = request.NewsId,

                Name = request.Name,

                Description = request.Description,

                SourceLink = request.SourceLink,

            };

            _context.News.Add(news);

            return await _context.SaveChangesAsync();

        }

        public async Task<int> Delete(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);

            if (news == null) throw new FakeNewsException($"Cannot find a News with Id: {newsId}");
            _context.News.Remove(news);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<NewsViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<NewsViewModel>> GetAllPading(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<NewsViewModel>> GetAllPading(GetNewsPagingRequest request)
        {
            //1. Select Join
            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            //2. Filter
            if(request.TopicIds.Count > 0)
            {
                query = query.Where(t => request.TopicIds.Contains(t.nit.TopicId));
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

        public async Task<int> Update(NewsUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLink(int newsId, string newLink)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMedia(int newsId, Data.Entities.Media newMedia)
        {
            throw new NotImplementedException();
        }
    }
}
