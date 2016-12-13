using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pushup.Startup))]
namespace Pushup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
