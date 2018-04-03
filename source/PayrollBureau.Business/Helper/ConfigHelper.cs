using System.Configuration;

namespace PayrollBureau.Business.Helper
{
    public static class ConfigHelper
    {
        public static string DefaultConnection => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
    }
}
