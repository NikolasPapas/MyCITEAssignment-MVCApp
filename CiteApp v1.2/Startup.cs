using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CiteApp_v1._2.Startup))]
namespace CiteApp_v1._2
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
