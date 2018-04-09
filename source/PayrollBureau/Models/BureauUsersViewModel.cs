using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayrollBureau.Business.Models;

namespace PayrollBureau.Models
{
    public class BureauUsersViewModel
    {
        public User User { get; set; }
        public int BureauId { get; set; }
        public string BureauName { get; set; }
    }
}