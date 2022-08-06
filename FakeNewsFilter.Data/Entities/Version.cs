using System;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.Data.Entities
{
    public class Version
    {
        public int VersionId { get; set; }

        public string VersionNumber { get; set; }

        public string Content { get; set; }

        public Platform Platform { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ReleaseTime { get; set; }

        public bool isRequired { get; set; }

        public VStatus Status { get; set; }

    }
}

