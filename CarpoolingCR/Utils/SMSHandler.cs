using Microsoft.AspNet.Identity;

namespace CarpoolingCR.Utils
{
    public class SMSHandler
    {
        public static void SendSMS(string countryCode, string phoneNumber, string text, string url)
        {
            var message = new IdentityMessage
            {
                Destination = phoneNumber.Replace("-", string.Empty),
                Body = text + "  " + url
            };

            new SmsService().SendAsync(message);
        }
    }
}