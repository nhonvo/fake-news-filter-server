using System;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.TopicNews
{
    public class TopicNewsCreateRequest
    {
        public string Label { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }
     
        public MediaType Type { get; set; }

        public string MediaLink { get; set; }

        public IFormFile ThumbnailMedia { get; set; }
    }
}
