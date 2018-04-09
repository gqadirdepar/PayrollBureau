﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayrollBureau.Models
{
    public class BaseViewModel
    {
        public int? EmployerId { get; set; }
        public int? BureauId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployerName { get; set; }
        public string BureauName { get; set; }
        public string EmployeeName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}