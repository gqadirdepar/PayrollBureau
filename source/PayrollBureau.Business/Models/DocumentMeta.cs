using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollBureau.Business.Models
{
    public class DocumentMeta
    {
        public int DocumentTypeId { get; set; }
        public int Id { get; set; }
        public int? EngagementId { get; set; }
        public string Engagement { get; set; }
        public string Type { get; set; }
        public DateTime UploadedDate { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; } // can be NULL when just retrieving descriptions
        public Guid DocumentGuid { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public string EmployeeName { get; set; }
        public string PayrollId { get; set; }
        public string DocumentType { get; set; }
    }
}
