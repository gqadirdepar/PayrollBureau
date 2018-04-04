using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollBureau.Models
{
    public class BaseViewModel
    {
        public int EmployerId { get; set; }
        public int BureauId { get; set; }
        public string EmployerName { get; set; }
        public string BureauName { get; set; }
    }
}