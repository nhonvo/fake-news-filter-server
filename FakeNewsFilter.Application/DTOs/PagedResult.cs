using System;
using System.Collections.Generic;

namespace FakeNewsFilter.Application.DTOs
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }

        public int TotalRecord { get; set; }
    }
}
