using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog.News.DTO.Manage;
using FakeNewsFilter.Application.DTOs;
using FakeNewsFilter.Data.Entities;

namespace FakeNewsFilter.Application.Catalog.News
{
    public interface IManageNewsService
    {
        Task<int> Create(NewsCreateRequest request);


        Task<int> Update(NewsUpdateRequest request);

        Task<bool> UpdateLink(int newsId, string newLink);

        Task<bool> UpdateMedia(int newsId, Media newMedia);

        Task<int> Delete(int NewsId);


        Task<List<NewsViewModel>> GetAll();


        Task<PagedResult<NewsViewModel>> GetAllPading(GetNewsPagingRequest request);
    }
}
