using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LavaWeb.Startup))]
namespace LavaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
