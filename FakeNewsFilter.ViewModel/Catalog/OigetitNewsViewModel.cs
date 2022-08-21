using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Catalog;

public class OigetitNews
{
    public object ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PubDate { get; set; }
    public string URL { get; set; }
    public int rssfeed { get; set; }
    public string Feed { get; set; }
    public string ImageURL { get; set; }
    public int Happiness { get; set; }
    public double Trusted { get; set; }
}

public class OigetitNewsViewModel
{
    public OigetitNewsViewModel()
    {
    }
    public OigetitNewsViewModel(string message) {
        Message = message;
    }
    public List<OigetitNews> result { get; set; }
    public int total_count { get; set; }
    public int page_count { get; set; }
    public string Message { get; set; }
}