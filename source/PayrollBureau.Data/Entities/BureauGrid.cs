namespace PayrollBureau.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BureauGrid")]
    public partial class BureauGrid
    {
        [Key]
        [Column(Order = 0)]
        public int BureauId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(256)]
        public string EmailId { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        public int? EmployerCount { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(756)]
        public string SearchTerm { get; set; }
    }
}
