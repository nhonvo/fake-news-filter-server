using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.AdminApp.Services
{
    public interface ITopicApiClient
    {
        Task<ApiResult<List<TopicInfoVM>>> GetTopicInfo();

        Task<ApiResult<bool>> CreateTopic(TopicCreateRequest request);
    }

}
