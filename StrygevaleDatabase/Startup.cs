using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StrygevaleDatabase.Startup))]
namespace StrygevaleDatabase
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
