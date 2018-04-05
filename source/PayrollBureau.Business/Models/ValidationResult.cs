using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollBureau.Business.Models
{
    public class ValidationResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }

    public class ValidationResult<T> : ValidationResult where T : class
    {
        public T Entity { get; set; }
    }
}
