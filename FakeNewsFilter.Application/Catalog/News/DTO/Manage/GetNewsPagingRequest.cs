using System;
using System.Collections.Generic;
using FakeNewsFilter.Application.DTOs;

namespace FakeNewsFilter.Application.Catalog.News.DTO.Manage
{
    public class GetNewsPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> TopicIds { get; set; }
    }
}
