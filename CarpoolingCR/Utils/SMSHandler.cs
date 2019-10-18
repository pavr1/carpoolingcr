using CarpoolingCR.Models;
using Microsoft.AspNet.Identity;

namespace CarpoolingCR.Utils
{
    public class SMSHandler
    {
        public static void SendSMS(ApplicationUser user, string text, string url, string logo)
        {
            var message = new IdentityMessage
            {
                Destination = user.Phone1.Replace("-", string.Empty),
                Body = text + "  " + url
            };

            new SmsService().SendAsync(user, message, logo);
        }

        public static void SendSMS(string phone, string text, string logo)
        {
            var message = new IdentityMessage
            {
                Destination = phone.Replace("-", string.Empty),
                Body = text
            };

            new SmsService().SendPromotionAsync(message, logo);
        }
    }
}