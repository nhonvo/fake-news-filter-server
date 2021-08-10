using System;
namespace FakeNewsFilter.Data.Entities
{
    public class Article
    {
        public int Id { get; set; }

        public string ArticleName { get; set; }

        public Media Media { get; set; }

    }
}
