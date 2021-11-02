using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.TopicNews
{
    public class TopicCreateRequest
    {
        public string Label { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public string LanguageId { get; set; }

        public IFormFile ThumbTopic { get; set; }
    }
}
