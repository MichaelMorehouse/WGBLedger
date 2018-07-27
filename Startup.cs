using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WGBLedger.Startup))]
namespace WGBLedger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
