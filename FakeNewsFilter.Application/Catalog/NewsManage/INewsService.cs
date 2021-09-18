using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.Application.Catalog.NewsManage
{
    public interface INewsService
    {
        Task<ApiResult<List<NewsViewModel>>> GetAll(string language);

        Task<ApiResult<List<NewsViewModel>>> GetNewsInTopic(GetPublicNewsRequest request);

        Task<ApiResult<int>> Create(NewsCreateRequest request);

        Task<ApiResult<bool>> Delete(int NewsId);

        Task<ApiResult<NewsViewModel>> GetById(int newsId);

        Task<ApiResult<bool>> Update(NewsUpdateRequest request);

        Task<ApiResult<bool>> UpdateLink(int newsId, string newLink);
    }
}