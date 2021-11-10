using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.ViewModel.Catalog.SourceStory
{
    public class SourceUpdateRequest
    {
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public string LanguageId { set; get; }
    }
}
