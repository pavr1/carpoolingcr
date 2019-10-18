using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using CarpoolingCR.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Configuration;
using CarpoolingCR.Utils;
using System.IO;
using System.Net.Mime;

namespace CarpoolingCR
{
    public class EmailService : IIdentityMessageService
    {
        private object webconfigurationmanager;

        public Task SendNotificationsAsync(IdentityMessage message, string logo)
        {
            MailMessage mail = new MailMessage(WebConfigurationManager.AppSettings["notificationsemail"], message.Destination, message.Subject, message.Body);
            mail.IsBodyHtml = true;

            var client = new SmtpClient(WebConfigurationManager.AppSettings["mailhost"], Convert.ToInt32(WebConfigurationManager.AppSettings["mailport"]))
            {
                Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["notificationsemail"], WebConfigurationManager.AppSettings["notificationspassword"]),
                EnableSsl = false
            };

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = message.Destination
                }, logo);
            }

            return Task.FromResult(0);
        }

        public Task SendErrorsAsync(IdentityMessage message, string logo)
        {
            MailMessage mail = new MailMessage(WebConfigurationManager.AppSettings["ErrorsEmail"], message.Destination, message.Subject, message.Body);
            mail.IsBodyHtml = true;

            var client = new SmtpClient(WebConfigurationManager.AppSettings["mailhost"], Convert.ToInt32(WebConfigurationManager.AppSettings["mailport"]))
            {
                Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["ErrorsEmail"], WebConfigurationManager.AppSettings["ErrorsPassword"]),
                EnableSsl = false
            };

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = message.Destination
                }, logo);
            }

            return Task.FromResult(0);
        }

        public Task SendInformativeAsync(string email, string subject, string description, string contentId, Stream picture, string contentType, int? picWidth, int? picHeight, string providerEmail, string providerPwd, bool bcc, string logo)
        {
            //string htmlBody = "<html><body><h1>Picture</h1><br><img src=\"cid:logo\"></body></html>";
            //AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            //LinkedResource inline = new LinkedResource(picture, MediaTypeNames.Image.Jpeg);
            //inline.ContentId = "logo";
            //avHtml.LinkedResources.Add(inline);

            //MailMessage mail = new MailMessage();
            //mail.AlternateViews.Add(avHtml);

            //mail.From = new MailAddress(providerEmail);
            //mail.To.Add("pavr1@hotmail.com");
            //mail.Subject = "Client: Has Sent You A Screenshot";
            //mail.Body = String.Format("<h3>Client: Has Sent You A Screenshot</h3>" + @"<img src=""cid:{0}"" />", "logo");

            //mail.IsBodyHtml = true;

            if (picWidth == null)
            {
                picWidth = 200;
            }
            if (picHeight == null)
            {
                picHeight = 200;
            }

            var htmlBody = "<!DOCTYPE html>";
            htmlBody += "<html>";
            htmlBody += "<head>";
            htmlBody += "<meta charset=\"utf -8\" />";
            htmlBody += "<title></title>";
            htmlBody += "</head>";
            htmlBody += "<body>";
            htmlBody += "<div class=\"jumbotron\">";
            htmlBody += "<div class=\"row visible-xs\">";
            htmlBody += "<div class=\"col -xs-12\">";
            htmlBody += "<img src=\"cid:logo\" class=\"center-block\" />";
            htmlBody += "<br><br>";
            htmlBody += "</div>";
            htmlBody += "</div>";

            htmlBody += "<div class=\"row\">";
            htmlBody += "<div class=\"col-lg-10 col-md-9 col-sm-8 col-xs-12\" style=\"text-align:justify\">";
            //htmlBody += "<b>{subject}</b><br><br>";
            htmlBody += "{Description}";
            htmlBody += "</div>";

            //if (picture != null)
            //{
            //    htmlBody += "<div class=\"col-lg-2 col-md-3 col-sm-4 hidden-xs pull-right\" style=\"width:" + picWidth + "px; height:" + picHeight + "px\">";
            //    htmlBody += "<img src=\"cid:{contentId}\" style=\"width:" + picWidth + "px; height:" + picHeight + "px\" class=\"center-block\" />";
            //    htmlBody += "</div>";
            //}

            htmlBody += "</div>";

            htmlBody += "<div class=\"row\">";
            htmlBody += "<div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\" style=\"text-align:justify\">";
            htmlBody += "¡Hagamos Ride!<br />";

            htmlBody += "<a href='www.buscoridecr.com'>www.buscoridecr.com</a>";
            htmlBody += "</div>";
            htmlBody += "</div>";
            htmlBody += "</div>";
            htmlBody += "</body>";
            htmlBody += "</html>";

            //get this from /Templates/EmailBody.html
            htmlBody = htmlBody.Replace("{subject}", subject);
            htmlBody = htmlBody.Replace("{Description}", description);
            htmlBody = htmlBody.Replace("{contentId}", contentId);

            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            LinkedResource inline = new LinkedResource(picture, MediaTypeNames.Image.Jpeg);
            inline.ContentId = "logo";
            inline.ContentType = new ContentType("image/jpeg");
            avHtml.LinkedResources.Add(inline);

            MailMessage mail = new MailMessage();
            mail.AlternateViews.Add(avHtml);
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(providerEmail);
            mail.To.Add(email);

            if (bcc)
            {
                mail.Bcc.Add(WebConfigurationManager.AppSettings["AdminEmails"]);
            }

            mail.Subject = subject;

            //MailMessage mail = new MailMessage();
            //mail.AlternateViews.Add(avHtml);
            //MailMessage mail = new MailMessage(providerEmail, email, subject, htmlBody);
            //mail.IsBodyHtml = true;
            ////contenttype = "image/jpeg"
            //var attachment = new Attachment(picture, contentType);
            //attachment.ContentId = contentId;

            //mail.Attachments.Add(attachment);

            var client = new SmtpClient(WebConfigurationManager.AppSettings["mailhost"], Convert.ToInt32(WebConfigurationManager.AppSettings["mailport"]))
            {
                Credentials = new NetworkCredential(providerEmail, providerPwd),
                EnableSsl = false
            };

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                //this will cycle error sending message
                //Common.LogData(new Log
                //{
                //    Line = Common.GetCurrentLine(),
                //    Location = Enums.LogLocation.Server,
                //    LogType = Enums.LogType.Error,
                //    Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
                //    Method = Common.GetCurrentMethod(),
                //    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                //    UserEmail = email
                //}, logo);
            }

            return Task.FromResult(0);
        }

        public Task SendInformativePictureAsync(string email, string subject, string description, string contentId, Stream picture, string contentType, int? picWidth, int? picHeight, string providerEmail, string providerPwd)
        {
            if (picWidth == null)
            {
                picWidth = 200;
            }
            if (picHeight == null)
            {
                picHeight = 200;
            }

            var htmlBody = "<!DOCTYPE html>";
            htmlBody += "<html>";
            htmlBody += "<head>";
            htmlBody += "<meta charset=\"utf -8\" />";
            htmlBody += "<title></title>";
            htmlBody += "</head>";
            htmlBody += "<body>";
            htmlBody += "<div class=\"jumbotron\">";
            htmlBody += "<div class=\"row visible-xs\">";
            htmlBody += "<div class=\"col -xs-12\">";
            htmlBody += "<img src=\"cid:logo\" class=\"center-block\" />";
            htmlBody += "<br><br>";
            htmlBody += "</div>";
            htmlBody += "</div>";

            htmlBody += "<div class=\"row\">";
            htmlBody += "<div class=\"col-lg-10 col-md-9 col-sm-8 col-xs-12\" style=\"text-align:justify\">";
            htmlBody += "<b>{subject}</b><br><br>";
            htmlBody += "</div>";
            htmlBody += "</div>";

            htmlBody += "<div class=\"row\">";
            htmlBody += "<div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\" style=\"text-align:justify\">";
            htmlBody += "¡Hagamos Ride!<br />";

            htmlBody += "<a href='www.buscoridecr.com'>www.buscoridecr.com</a>";
            htmlBody += "</div>";
            htmlBody += "</div>";
            htmlBody += "</div>";
            htmlBody += "</body>";
            htmlBody += "</html>";

            //get this from /Templates/EmailBody.html
            htmlBody = htmlBody.Replace("{subject}", subject);
            htmlBody = htmlBody.Replace("{Description}", description);
            htmlBody = htmlBody.Replace("{contentId}", contentId);

            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            LinkedResource inline = new LinkedResource(picture, MediaTypeNames.Image.Jpeg);
            inline.ContentId = "logo";
            inline.ContentType = new ContentType("image/jpeg");
            avHtml.LinkedResources.Add(inline);

            MailMessage mail = new MailMessage();
            mail.AlternateViews.Add(avHtml);
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(providerEmail);
            mail.To.Add(email);
            mail.Bcc.Add(WebConfigurationManager.AppSettings["AdminEmails"]);
            mail.Subject = subject;

            var client = new SmtpClient(WebConfigurationManager.AppSettings["mailhost"], Convert.ToInt32(WebConfigurationManager.AppSettings["mailport"]))
            {
                Credentials = new NetworkCredential(providerEmail, providerPwd),
                EnableSsl = false
            };

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                //this will cycle error sending message
                //Common.LogData(new Log
                //{
                //    Line = Common.GetCurrentLine(),
                //    Location = Enums.LogLocation.Server,
                //    LogType = Enums.LogType.Error,
                //    Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
                //    Method = Common.GetCurrentMethod(),
                //    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                //    UserEmail = email
                //}, logo);
            }

            return Task.FromResult(0);
        }

        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(ApplicationUser user, IdentityMessage message, string logo)
        {
            var api = WebConfigurationManager.AppSettings["SMSApi"];
            var apiFilters = "t=" + message.Destination + "&m=" + message.Body;

            api += apiFilters;

            WebRequest request;
            request = WebRequest.Create(api);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var result = reader.ReadToEnd();

            Common.LogData(new Log
            {
                Line = Common.GetCurrentLine(),
                Location = Enums.LogLocation.Server,
                LogType = Enums.LogType.SMS,
                Message = "Código de verificación " + message.Destination + " SMS enviado a " + user.FullName + ". Resultado: " + result,
                Method = Common.GetCurrentMethod(),
                Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                UserEmail = user.Name
            }, logo);

            return Task.FromResult(0);
        }

        public Task SendPromotionAsync(IdentityMessage message, string logo)
        {
            var api = WebConfigurationManager.AppSettings["SMSApi"];
            var apiFilters = "t=" + message.Destination + "&m=" + message.Body;

            api += apiFilters;

            WebRequest request;
            request = WebRequest.Create(api);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var result = reader.ReadToEnd();

            Common.LogData(new Log
            {
                Line = Common.GetCurrentLine(),
                Location = Enums.LogLocation.Server,
                LogType = Enums.LogType.SMS,
                Message = "Mensaje promocional enviado a " + message.Destination,
                Method = Common.GetCurrentMethod(),
                Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                UserEmail = WebConfigurationManager.AppSettings["ContactUsEmail"]
            }, logo);

            return Task.FromResult(0);
        }

        [Obsolete]
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
