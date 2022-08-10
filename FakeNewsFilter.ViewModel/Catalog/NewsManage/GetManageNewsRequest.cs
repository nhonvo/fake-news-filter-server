using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class GetManageNewsRequest
    {
        public string Keyword {get; set; }
        public List<int> TopicIds { get; set; }
    }
}
