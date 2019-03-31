using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CarpoolingCR.Utils
{
    public class Whatsapp
    {
        public static void SendMessage()
        {
            // Find your Account Sid and Token at twilio.com/console
            const string accountSid = "ACac7fb20f017deeccacc38fd0f7f127fc";
            const string authToken = "676be326d1529f9ae9dc4e4995c17f39";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Hello there!",
                from: new Twilio.Types.PhoneNumber("whatsapp:+(506)88443317"),
                to: new Twilio.Types.PhoneNumber("whatsapp:+(506)88443317")
            );

            Console.WriteLine(message.Sid);
        }

        //private void SendMessage2()
        //{
        //    var client = new RestClient("https://www.waboxapp.com/api/send/chat");
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("content-type", "application/x-www-form-urlencoded");
        //    request.AddParameter("application/x-www-form-urlencoded", "token=my-test-api-key&uid=12025550123&to=12025550193&custom_uid=msg-3848&text=Hello+world%21", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //}
    }
}