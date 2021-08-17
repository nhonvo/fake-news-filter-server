using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.Media;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.Application.Catalog.NewsManage
{
    public interface IManageNewsService
    {
        Task<int> Create(NewsCreateRequest request);

        Task<int> Delete(int NewsId);

        Task<NewsViewModel> GetById(int newsId);

        Task<int> Update(NewsUpdateRequest request);

        Task<bool> UpdateLink(int newsId, string newLink);
    }
}