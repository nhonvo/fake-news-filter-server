namespace FakeNewsFilter.ViewModel.Catalog.Notification;

public partial class Contents
{
    public string en { get; set; }
    public string vi { get; set; }
}

public partial class Headings
{
    public string en { get; set; }
    public string vi { get; set; }
}

public partial class Filter
{
    public string key { get; set; }
    public string field { get; set; }
    public string value { get; set; }
    public string relation { get; set; }
}