using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StokedWebAPI7.Startup))]
namespace StokedWebAPI7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
