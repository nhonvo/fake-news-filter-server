using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace FakeNewsFilter.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public List<Follow> Follows { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }
    }
}