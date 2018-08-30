using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(APMKommune.Startup))]
namespace APMKommune
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
