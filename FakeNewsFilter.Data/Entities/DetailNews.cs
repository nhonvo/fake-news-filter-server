using System;
namespace FakeNewsFilter.Data.Entities
{
	public class DetailNews
	{
		public int DetailNewsId { get; set; }

		public string Alias { get; set; }

		public string Content { get; set; }

		public int? ThumbNews { get; set; }

		public Media Media { get; set; }

		public News News { get; set; }
	}
}

