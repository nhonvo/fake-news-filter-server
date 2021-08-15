﻿using System;
using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }

        public int TotalRecord { get; set; }
    }
}
