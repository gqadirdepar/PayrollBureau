using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PayrollBureau.Startup))]
namespace PayrollBureau
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
