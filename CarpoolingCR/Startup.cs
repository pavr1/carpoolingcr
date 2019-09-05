using Microsoft.Owin;
using Owin;
using System.Globalization;
using System.Threading;

[assembly: OwinStartupAttribute(typeof(CarpoolingCR.Startup))]
namespace CarpoolingCR
{
    public partial class Startup
    {
        public async void Configuration(IAppBuilder app)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            app.MapSignalR();

            ConfigureAuth(app);
        }
    }
}
