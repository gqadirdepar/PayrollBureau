using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollBureau.Business.Models
{
    public class Bureau
    {
        public int BureauId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string CreatedBy { get; set; }
    }
}
