using System;
using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { get; set; }
    }
}
