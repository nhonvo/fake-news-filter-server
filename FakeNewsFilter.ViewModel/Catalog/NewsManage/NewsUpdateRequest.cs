using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class NewsUpdateRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public MediaType Type { get; set; }

        public IFormFile ThumbnailMedia { get; set; }

    }
}
