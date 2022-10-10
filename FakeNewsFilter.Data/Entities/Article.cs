using System;

namespace FakeNewsFilter.Data.Entities
{
    public class Article
    {
        public string ArticleName { get; set; }
        public int Id { get; set; }
        public Media Media { get; set; }
    }
}