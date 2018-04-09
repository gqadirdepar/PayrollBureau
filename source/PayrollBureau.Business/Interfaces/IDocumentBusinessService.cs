using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollBureau.Business.Models;
using PayrollBureau.Data.Entities;

namespace PayrollBureau.Business.Interfaces
{
    public interface IDocumentBusinessService
    {
       ValidationResult<EmployeeDocument> CreateEmployeeDocument(DocumentMeta documentMeta, int employeeId, string userId);
    }
}
