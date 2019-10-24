using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MikeBugTracker.Startup))]
namespace MikeBugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
