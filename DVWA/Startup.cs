using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DVWA.Startup))]
namespace DVWA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}