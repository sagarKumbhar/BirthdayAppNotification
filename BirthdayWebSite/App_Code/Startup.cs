using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BirthdayWebSite.Startup))]
namespace BirthdayWebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
