using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AptManager.Startup))]
namespace AptManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
