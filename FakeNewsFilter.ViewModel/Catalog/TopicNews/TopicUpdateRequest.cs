﻿using System;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.TopicNews
{
    public class TopicUpdateRequest
    {
        public int TopicId { get; set; }

        public string Label { get; set; }

        public string Tag { get; set; }

        public string LanguageId { set; get; }

        public string Description { get; set; }

        public IFormFile ThumbImage { get; set; }
    }
}
