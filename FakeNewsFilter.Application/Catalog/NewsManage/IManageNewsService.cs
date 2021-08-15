
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.News;
using FakeNewsFilter.ViewModel.Catalog.News.Manage;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.Application.Catalog.NewsManage
{
    public interface IManageNewsService
    {
        Task<int> Create(NewsCreateRequest request);


        Task<int> Update(NewsUpdateRequest request);

        Task<bool> UpdateLink(int newsId, string newLink);

        Task<int> Delete(int NewsId);

        Task<PagedResult<NewsViewModel>> GetAllPaging(GetNewsPagingRequest request);

    }
}
