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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            EmployeeDocuments = new HashSet<EmployeeDocument>();
        }

        public int EmployeeId { get; set; }

        public int EmployerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        public int? EmployeeCode { get; set; }

        public int? PayrollId { get; set; }

        [Required]
        [StringLength(256)]
        public string EmailId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDateUtc { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(128)]
        public string AspnetUserId { get; set; }

        public virtual Employer Employer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeDocument> EmployeeDocuments { get; set; }
    }
}
