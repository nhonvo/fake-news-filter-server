﻿using System;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.ViewModel.Catalog.Version
{
    public class VersionCreateRequest
    {
        public string VersionNumber { get; set; }

        public string Content { get; set; }

        public string Platform { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ReleaseTime { get; set; }

        public bool isRequired { get; set; }

        public VStatus Status { get; set; }
    }
}

