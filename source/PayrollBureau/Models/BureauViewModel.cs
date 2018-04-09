using System.ComponentModel.DataAnnotations;
using PayrollBureau.Data.Entities;

namespace PayrollBureau.Models
{
    public class BureauViewModel:BaseViewModel
    {
        public Bureau Bureau { get; set; }
    }
}