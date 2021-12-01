using System;
namespace FakeNewsFilter.ViewModel.Catalog.Language
{
    public class GetLanguageRequest
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        public string Flag { get; set; }
        public bool IsDefault { get; set; }
    }
}
