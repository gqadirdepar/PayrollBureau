using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollBureau.Data.Entities
{
    [Table("Bureau")]
    public partial class Bureau
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bureau()
        {
            Employers = new HashSet<Employer>();
        }

        public int BureauId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDateUtc { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(128)]
        public string AspnetUserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employer> Employers { get; set; }
    }
}
