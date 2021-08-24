﻿using System;
using System.Collections.Generic;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }

        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}
