using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OFamiliar.Startup))]
namespace OFamiliar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
