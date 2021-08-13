using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace FakeNewsFilter.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public Status Status { get; set; }

        public List<Follow> Follows { get; set; }
    }
}
