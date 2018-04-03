using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PayrollBureau.Data.Models
{
    public class OrderBy
    {
        public string Class { get; set; }
        public string Property { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ListSortDirection Direction { get; set; }        
    }
}
