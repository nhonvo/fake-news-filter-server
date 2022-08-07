using System;
namespace FakeNewsFilter.ViewModel.Catalog.Claims
{
    public class Publisher
    {
        public string Name { get; set; }

        public string Site { get; set; }
    }

    public class ClaimReviewViewModel
    {
        public Publisher Publisher { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string TextualRating { get; set; }

        public string LanguageCode { get; set; }

    }
}