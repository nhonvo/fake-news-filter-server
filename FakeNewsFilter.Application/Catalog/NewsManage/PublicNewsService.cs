using System;
using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using System.Collections.Generic;
using FakeNewsFilter.ViewModel.Catalog.Media;
using AutoMapper;

namespace FakeNewsFilter.Application.Catalog.NewsManage
{
    public class PublicNewsService : IPublicNewsService
    {
        private readonly ApplicationDBContext _context;

        private readonly IMapper _mapper;

        public PublicNewsService(ApplicationDBContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Get All News
        public async Task<List<NewsViewModel>> GetAll()
        {
            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            var data = await query
               .Select(x => new NewsViewModel()
               {
                   NewsId = x.n.NewsId,
                   Name = x.n.Name,
                   TopicId = x.c.TopicId,
                   LabelTopic = x.c.Label,
                   Description = x.c.Description,
                   SourceLink = x.n.SourceLink,
                   Media = _mapper.Map<MediaViewModel>(x.n.Media),
                   Timestamp = x.n.Timestamp,
               }).ToListAsync();

            return data;
        }

        //Get All News Paging
        public async Task<PagedResult<NewsViewModel>> GetAllPaging(GetManageNewsRequest request)
        {
            //1. Select Join
            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            //2. Filter
            if (request.TopicIds.Count > 0)
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
                    LabelTopic = x.c.Label,

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<NewsViewModel>()
            {
                TotalRecord = TotalRow,
                Items = data
            };

            return pagedResult;
        }

        public async Task<PagedResult<NewsViewModel>> GetAllByTopicId(GetPublicNewsRequest request)
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
                    LabelTopic = x.c.Label,

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<NewsViewModel>()
            {
                TotalRecord = TotalRow,
                Items = data
            };

            return pagedResult;
        }

        //Get All News In Topic (Limit 50)
        public async Task<List<NewsViewModel>> GetNewsInTopic(GetPublicNewsRequest request)
        {

            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };


            query = query.Where(t => request.TopicId == t.nit.TopicId);


            var data = await query
                .Select(x => new NewsViewModel()
                {
                    NewsId = x.n.NewsId,
                    Name = x.n.Name,
                    TopicId = x.c.TopicId,
                    LabelTopic = x.c.Label,
                    Description = x.c.Description,
                    SourceLink = x.n.SourceLink,
                    Media = _mapper.Map<MediaViewModel>(x.n.Media),
                    Timestamp = x.n.Timestamp,
                }).ToListAsync();


            return data;
        }

       
    }
}
