using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtistCatalog.Web.Startup))]
namespace ArtistCatalog.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
