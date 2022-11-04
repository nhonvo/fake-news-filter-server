using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Catalog.Notification;

public class CreateNotification
{
    public string app_id { get; set; }
    public string name { get; set; }
    public string included_segments { get; set; }
    public string url { get; set; }
    public Headings headings { get; set; }
    public Contents contents { get; set; }
    public List<Filter> filters { get; set; }
}