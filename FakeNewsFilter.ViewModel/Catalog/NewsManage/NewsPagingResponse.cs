using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage;

public class NewsPagingResponse
{
    public List<NewsViewModel> Items { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int PageCount { get; set; }
}