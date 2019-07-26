using CarpoolingCR.Utils;
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
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            app.MapSignalR();

            ConfigureAuth(app);
        }
    }
}
