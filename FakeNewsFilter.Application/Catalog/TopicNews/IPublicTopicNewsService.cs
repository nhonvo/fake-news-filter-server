using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;

namespace FakeNewsFilter.Application.Catalog.Topic
{
    public interface IPublicTopicNewsService
    {
        public Task<List<TopicNewsViewModel>> GetTopicHotNews();
    }
}