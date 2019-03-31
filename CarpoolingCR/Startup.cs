using Microsoft.Owin;
using Owin;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

[assembly: OwinStartupAttribute(typeof(CarpoolingCR.Startup))]
namespace CarpoolingCR
{
    public partial class Startup
    {
        public async void Configuration(IAppBuilder app)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-CR");
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-CR");

            app.MapSignalR();

            ConfigureAuth(app);
        }
    }
}
