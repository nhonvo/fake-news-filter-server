using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.Application.Catalog.TopicNews
{
    public interface ITopicService
    {
        Task<ApiResult<List<TopicInfoVM>>> GetTopicHotNews();

        Task<ApiResult<bool>> Create(TopicCreateRequest request);

        Task<ApiResult<TopicInfoVM>> GetTopicById(int Id);

        Task<ApiResult<bool>> Delete(int TopicId);

        Task<ApiResult<bool>> Update(TopicUpdateRequest request);
    }
}