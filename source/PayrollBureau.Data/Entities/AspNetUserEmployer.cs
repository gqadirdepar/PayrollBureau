namespace PayrollBureau.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspNetUserEmployer")]
    public partial class AspNetUserEmployer
    {
        public int AspNetUserEmployerId { get; set; }

        public int EmployerId { get; set; }

        [Required]
        [StringLength(128)]
        public string AspNetUserId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Employer Employer { get; set; }
    }
}
