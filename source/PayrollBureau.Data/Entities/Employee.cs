namespace PayrollBureau.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public int EmployeeId { get; set; }

        public int EmployerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string PayrollNumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDateUtc { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        [StringLength(128)]
        public string AspnetUserId { get; set; }

        public virtual Employer Employer { get; set; }
    }
}
