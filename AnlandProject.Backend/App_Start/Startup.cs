using AnlandProject.Backend.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace AnlandProject.Backend.App_Start
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app); //設定網站驗證方式
        }
    }
}