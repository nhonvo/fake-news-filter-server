using System;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.Data.Entities
{
    public class Media
    {
        public int Id { get; set; }

        public MediaType Type { get; set; }

        public String Url { get; set; }

        public int Duration { get; set; }
    }
}
