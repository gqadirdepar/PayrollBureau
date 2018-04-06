using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PayrollBureau.Business.Models
{
    public class User
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Username { get; set; }

        [EmailAddress]
        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

    }
}
