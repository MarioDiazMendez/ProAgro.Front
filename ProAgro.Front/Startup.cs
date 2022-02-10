using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProAgro.Front.Startup))]
namespace ProAgro.Front
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
