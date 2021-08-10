using System;
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
        [Key]
        [Display(Name = "User Id")]
        public Guid UserId { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Password { get; set; }

        public Status Status { get; set; }
    }
}
