namespace PayrollBureau.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeGrid")]
    public partial class EmployeeGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployerId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string PayrollNumber { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "datetime2")]
        public DateTime CreatedDateUtc { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        [StringLength(128)]
        public string AspnetUserId { get; set; }

        [StringLength(100)]
        public string EmployerName { get; set; }

        public int? BureauId { get; set; }

        [StringLength(100)]
        public string BureauName { get; set; }

        public string Email { get; set; }
    }
}
