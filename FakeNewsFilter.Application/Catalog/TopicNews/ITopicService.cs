using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.Application.Catalog.TopicNews
{
    public interface ITopicService
    {
        public Task<ApiResult<List<TopicInfoVM>>> GetTopicHotNews();

        Task<ApiResult<bool>> Create(TopicNewsCreateRequest request);

        Task<ApiResult<bool>> Delete(int TopicId);

        Task<ApiResult<bool>> Update(TopicNewsUpdateRequest request);
    }
}