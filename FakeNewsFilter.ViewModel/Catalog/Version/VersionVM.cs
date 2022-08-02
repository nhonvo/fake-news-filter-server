using System;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.ViewModel.Catalog.Version
{
    public class VersionVM
    {
        public int VersionId { get; set; }

        public float VersionNumber { get; set; }

        public string Content { get; set; }

        public string Platform { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ReleaseTime { get; set; }

        public bool isRequired { get; set; }

        public string Status { get; set; }
    }
}

