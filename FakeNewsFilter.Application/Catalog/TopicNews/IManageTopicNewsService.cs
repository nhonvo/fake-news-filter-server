using System;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;

namespace FakeNewsFilter.Application.Catalog.TopicNews
{
    public interface IManageTopicNewsService
    {
        Task<int> Create(TopicNewsCreateRequest request);

        Task<int> Delete(int TopicId);

        Task<int> Update(TopicNewsUpdateRequest request);
    }
}