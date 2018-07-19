using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudiekollenNew.Startup))]
namespace StudiekollenNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
