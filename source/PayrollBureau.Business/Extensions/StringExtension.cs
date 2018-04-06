using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PayrollBureau.Business.Extensions
{
    public static class StringExtension
    {
        public static string FilterSpecialChars(this string @string)
        {
            return Regex.Replace(@string, @"[^0-9a-zA-Z.]+", "_"); //replace anything other than 0-9/a-z/A-Z . with _

        }
    }
}
