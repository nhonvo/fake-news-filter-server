using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FakeNewsFilter.Data.Entities
{
    [NotMapped]
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}