namespace PayrollBureau.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeDocument")]
    public partial class EmployeeDocument
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeDocumentId { get; set; }

        public int EmployeeId { get; set; }

        public int DocumentCategoryId { get; set; }

        public Guid DocumentGuid { get; set; }

        public DateTime? ValidFromDate { get; set; }

        public DateTime? ValidToDate { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedDateUtc { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
