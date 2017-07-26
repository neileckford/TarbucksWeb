using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TarbucksWeb.Startup))]
namespace TarbucksWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
