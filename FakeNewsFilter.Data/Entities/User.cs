﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.Data.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
         
        public string Password { get; set; }

        public Status Status { get; set; }

        public List<Follow> Follows { get; set; }
    }
}
