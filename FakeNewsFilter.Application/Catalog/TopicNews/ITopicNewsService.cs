using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;

namespace FakeNewsFilter.Application.Catalog.TopicNews
{
    public interface ITopicNewsService
    {
        public Task<List<TopicNewsViewModel>> GetTopicHotNews();

        Task<int> Create(TopicNewsCreateRequest request);

        Task<int> Delete(int TopicId);

        Task<int> Update(TopicNewsUpdateRequest request);
    }
}