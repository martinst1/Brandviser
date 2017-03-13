using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Brandviser.Web.Startup))]
namespace Brandviser.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
