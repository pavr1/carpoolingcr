using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace CarpoolingCR.Utils
{
    [HubName("SignalHandlerHub")]
    public class SignalHandler: Hub
    {
        public void SendMessage(string aspNetUserid, string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SignalHandler>();
            context.Clients.All.broadcastMessage(aspNetUserid, message);
        }
    }
}