using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Common;


namespace FakeNewsFilter.Application.Catalog.NewsManage
{
    public interface IPublicNewsService
    {
        Task<List<NewsViewModel>> GetAll();

        Task<PagedResult<NewsViewModel>> GetAllPaging(GetManageNewsRequest request);


        public Task<List<NewsViewModel>> GetNewsInTopic(GetPublicNewsRequest request);


    }
}
