using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Catalog.Notification
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Contents
    {
        public string en { get; set; }
        public string vi { get; set; }
    }

    public class Headings
    {
        public string en { get; set; }
        public string vi { get; set; }
    }

    public class Filter
    {
        public string key { get; set; }
        public string field { get; set; }
        public string value { get; set; }
        public string relation { get; set; }
    }

    public class Android
    {
        public int successful { get; set; }
        public int failed { get; set; }
        public int errored { get; set; }
        public int converted { get; set; }
        public int received { get; set; }
    }

    public class Ios
    {
        public int successful { get; set; }
        public int failed { get; set; }
        public int errored { get; set; }
        public int converted { get; set; }
        public int received { get; set; }
    }

    public class PlatformDeliveryStats
    {
        public Android android { get; set; }
        public Ios ios { get; set; }
    }

    public class Notification
    {
        public bool canceled { get; set; }
        public Contents contents { get; set; }
        public int converted { get; set; }
        public object data { get; set; }
        public object delayed_option { get; set; }
        public object delivery_time_of_day { get; set; }
        public int errored { get; set; }
        public List<object> excluded_segments { get; set; }
        public int failed { get; set; }
        public Headings headings { get; set; }
        public string id { get; set; }
        public object include_player_ids { get; set; }
        public object include_external_user_ids { get; set; }
        public List<object> included_segments { get; set; }
        public int queued_at { get; set; }
        public int remaining { get; set; }
        public int send_after { get; set; }
        public int completed_at { get; set; }
        public int successful { get; set; }
        public object received { get; set; }
        public object tags { get; set; }
        public List<Filter> filters { get; set; }
        public PlatformDeliveryStats platform_delivery_stats { get; set; }
        public string name { get; set; }
    }

    public class GetViewNotifications
    {
        public GetViewNotifications(string message) {
            Message = message;
        }
        public string Message { get; set; }
        public int total_count { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public List<Notification> notifications { get; set; }
    }
    
}