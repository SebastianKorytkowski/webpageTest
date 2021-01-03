using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webpageTest.Startup))]
namespace webpageTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
