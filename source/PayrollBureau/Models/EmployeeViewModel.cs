using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PayrollBureau.Data.Entities;

namespace PayrollBureau.Models
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public int EmployerId { get; set; }
        public string EmployerName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
