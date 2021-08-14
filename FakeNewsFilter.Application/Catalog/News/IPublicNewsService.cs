using System;
using FakeNewsFilter.Application.Catalog.News.DTO.Manage;
using FakeNewsFilter.Application.Catalog.News.DTO;
using FakeNewsFilter.Application.DTOs;

namespace FakeNewsFilter.Application.Catalog.News
{
    public interface IPublicNewsService
    {

        PagedResult<NewsViewModel> GetAllByTopicId(GetNewsPagingRequest request);

    }
}
