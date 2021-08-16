using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog.Topic;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog.TopicNews
{
    public class PublicTopicNewsService : IPublicTopicNewsService
    {
        private readonly ApplicationDBContext _context;


        public PublicTopicNewsService(ApplicationDBContext context)
        {
            _context = context;
        }

        //Get 10 Topic News Hot
        public async Task<List<TopicNewsViewModel>> GetTopicHotNews()
        {
            var query = _context.TopicNews.Select(x => x).Take(10);

            var topics = await query.Select(x => new TopicNewsViewModel()
            {
                TopicId = x.TopicId,
                Label = x.Label,
                Tag = x.Tag,
                Description = x.Description,
            }).ToListAsync();

            return topics;

        }
    }
}
