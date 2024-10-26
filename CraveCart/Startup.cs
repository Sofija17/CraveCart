using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CraveCart.Startup))]
namespace CraveCart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
