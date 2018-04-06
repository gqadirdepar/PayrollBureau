namespace PayrollBureau.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspNetUserBureau")]
    public partial class AspNetUserBureau
    {
        public int AspNetUserBureauId { get; set; }

        public int BureauId { get; set; }

        [Required]
        [StringLength(128)]
        public string AspNetUserId { get; set; }

    }
}
