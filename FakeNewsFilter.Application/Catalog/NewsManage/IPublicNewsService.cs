using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Common;


namespace FakeNewsFilter.Application.Catalog.NewsManage
{
    public interface IPublicNewsService
    {

        Task<PagedResult<NewsViewModel>> GetAllByTopicId(GetPublicNewsPagingRequest request);

    }
}
