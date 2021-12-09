using Microsoft.Owin;
using Owin;

//[assembly: OwinStartupAttribute(typeof(Ambucare.Startup))]
[assembly: OwinStartup(typeof(Ambucare.Startup))]
namespace Ambucare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
