using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class GetManageNewsRequest : PagingBase
    {
        public string Keyword {get; set; }

        public string LanguageId { get; set; }
    }
}
