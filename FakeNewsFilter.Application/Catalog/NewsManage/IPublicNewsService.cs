using System;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.Catalog.News;
using FakeNewsFilter.ViewModel.Catalog.News.Public;

namespace FakeNewsFilter.Application.Catalog.NewsManage
{
    public interface IPublicNewsService
    {

        Task<PagedResult<NewsViewModel>> GetAllByTopicId(GetNewsPagingRequest request);

    }
}
