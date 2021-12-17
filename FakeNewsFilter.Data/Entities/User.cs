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

        public int? AvatarId { get; set; }

        public Media Avatar { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }

        public List<Vote> Vote { get; set; }
        public List<TopicNews> TopicId { get; set; }
        public List <Comment> Comment { get; set; }
    }
}