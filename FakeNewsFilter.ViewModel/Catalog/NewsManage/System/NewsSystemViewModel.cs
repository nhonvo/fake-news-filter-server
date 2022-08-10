using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage;

public class NewsSystemViewModel
{
    public string LanguageId { get; set; }

    public int NewsId { get; set; }

    public string Title { get; set; }
    
    public string Content { get; set; }

    public String Alias { get; set; }

    public string ThumbNews { get; set; }

    public string OfficialRating { get; set; }

    public string Publisher { get; set; }

    public DateTime Timestamp { get; set; }

    public Status Status { get; set; }

    public List<TopicInfo> TopicInfo { get; set; }

    public string SourceCreate { get; set; }
}