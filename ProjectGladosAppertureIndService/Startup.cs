using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables.Config;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProjectGladosAppertureIndService.Startup))]

namespace ProjectGladosAppertureIndService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
             ConfigureMobileApp(app);
        }
    }
}