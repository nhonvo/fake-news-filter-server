using System;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.Feedback
{
    public class CreateFeedbackRequest
    {
       
        public int? NewsId { get; set; }

        public Guid? UserId { get; set; }

        public string Content { get; set; }

        public IFormFile? ScreenShoot { get; set; }
    }
}

