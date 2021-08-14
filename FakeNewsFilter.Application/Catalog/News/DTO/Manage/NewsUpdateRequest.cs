using System;
using FakeNewsFilter.Data.Entities;

namespace FakeNewsFilter.Application.Catalog.News.DTO.Manage
{
    public class NewsUpdateRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}
