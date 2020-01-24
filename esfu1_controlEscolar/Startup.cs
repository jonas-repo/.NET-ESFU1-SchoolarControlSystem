using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(esfu1_controlEscolar.Startup))]
namespace esfu1_controlEscolar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
