using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Catalog.Notification;

public class CreateNotificationRequest
{
    public string name { get; set; }
    public Headings headings { get; set; }
    public Contents contents { get; set; }
    public string filter { get; set; }
}

