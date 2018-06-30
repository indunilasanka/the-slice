using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheSlice.Startup))]
namespace TheSlice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
