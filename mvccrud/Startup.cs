using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCCrud.Startup))]
namespace MVCCrud
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
