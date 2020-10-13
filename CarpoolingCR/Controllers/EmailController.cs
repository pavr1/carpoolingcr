using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Configuration;
using System.Web.Mvc;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Controllers
{
    public class EmailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Email
        public ActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Success = message;
            }

            var user = Common.GetUserByEmail(User.Identity.Name);
            Common.UpdateMenuItemsCount(user.Id);

            return View();
        }

        public ActionResult SendEmailToAll()
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            try
            {
                //if (Request.Files.Count > 0)
                //{
                //    var file = Request.Files[0];

                //    if (file != null && file.ContentLength > 0)
                //    {
                //        var fnSplit = Path.GetFileName(file.FileName).Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                //        var fileName = "NotificationEmail_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss") + "." + fnSplit[1];

                //        string relativePath = "~/Content/Pictures/GroupEmails/" + fileName;
                //        string absolutePath = Server.MapPath(relativePath);

                //        try
                //        {
                //            file.SaveAs(absolutePath);
                //        }
                //        catch (Exception ex)
                //        {
                //            throw new Exception("Error saving image. Relative Path: " + relativePath + ". Absolute Path: " + absolutePath + ". Error: " + ex.Message);
                //        }

                var sendToAdminOnly = Request["send-to-admin-only"] == "on";

                List<ApplicationUser> users = new List<ApplicationUser>();

                if (sendToAdminOnly)
                {
                    users = db.Users.Where(x => x.UserType == UserType.Administrador).ToList();
                }
                else
                {
                    users = db.Users.Where(x => x.Status == Enums.ProfileStatus.Active).ToList();
                }

                PrepareEmailListToSend(users);
                //    }
                //}
                //else
                //{
                //    Common.LogData(new Log
                //    {
                //        Line = Common.GetCurrentLine(),
                //        Location = Enums.LogLocation.Server,
                //        LogType = Enums.LogType.Error,
                //        Message = "Attaching file not found!",
                //        Method = Common.GetCurrentMethod(),
                //        Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                //        UserEmail = User.Identity.Name,
                //        Fields = string.Empty
                //    }, logo);
                //}

                //¡Procesando de envío a todos los usuarios!
                return RedirectToAction("Index", new { message = "100062" });
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
                    UserEmail = User.Identity.Name,
                    Fields = string.Empty
                }, logo);

                return View();
            }
        }

        private List<ApplicationUser> _RemainingEmailsToSend = new List<ApplicationUser>();
        private List<ApplicationUser> _currentEmailsToSend = new List<ApplicationUser>();
        private System.Threading.Timer _timer = null;
        /// <summary>
        /// This method will split email list into bunch of 30 emails each and will send them all separately
        /// Email provider limits 50 emails per hour so if the app needs to send emails to all users, it will send 30 per hour
        /// It doesn't even matter when emails arrive to their destination, as long as they do
        /// </summary>
        /// <param name="users"></param>
        private void PrepareEmailListToSend(List<ApplicationUser> users)
        {
            _RemainingEmailsToSend = users;
            var massiveEmailWaitTime = Convert.ToInt32(WebConfigurationManager.AppSettings["AmountOfEmailsToSendPerHour"]);

            _timer = new System.Threading.Timer(timer1_Tick, null, 1000, massiveEmailWaitTime);
        }

        private void timer1_Tick(object sender)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            var lastEmailsToSend = false;
            var amountPerHour = Convert.ToInt32(WebConfigurationManager.AppSettings["AmountOfEmailsToSendPerHour"]);
            var startIndex = 0;
            var count = amountPerHour;

            if (_RemainingEmailsToSend.Count <= amountPerHour)
            {
                _currentEmailsToSend = _RemainingEmailsToSend;
                _RemainingEmailsToSend = new List<ApplicationUser>();
                lastEmailsToSend = true;
            }
            else
            {
                _currentEmailsToSend = _RemainingEmailsToSend.GetRange(startIndex, count);
                _RemainingEmailsToSend.RemoveRange(0, count);
            }

            var sendEmails = new Thread(() => SendToAll(_currentEmailsToSend));
            sendEmails.Start();

            //when last emails sent, dispose timer
            if (lastEmailsToSend)
            {
                _timer.Dispose();
                _timer = null;

                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Info,
                    Message = "Email sending process finished. Timer disposed!",
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = "",
                    Fields = string.Empty
                }, logo);
            }
        }

        private void SendToAll(List<ApplicationUser> users)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            try
            {
                string message = "<div class=\"row form-group\">" +
                                 "<div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\">" +
                                 "<div class=\"panel panel-default\" style=\"text-align:justify; border-radius:10px; padding:20px; margin:20px\">" +
                                 "<h4>" +
                                 "Promoción 2020" +
                                 "</h4>" +
                                 "<hr />" +
                                 "Gánate un bono de ₡1.000,00 con buscoridecr.com al crear tus viajes o al reservar." +
                                 "<br />" +
                                 "<br />" +
                                 "Además entra a tu <a href=\"https://buscoridecr.com/Manage/ProfileInfo\">Perfíl</a>, copia tu link de referencia y envíalo a tus contactos. Por cada contacto que se registre y confirme su correo electrónico bajo tu link de referencia, recibirás ₡1.000,00 de bono extra." +
                                 " También, los ususarios de nuevo ingreso recibirán ₡1.000,00 de bienvenida." +
                                 "<br />" +
                                 "<br />" +
                                 "Para más información escríbenos al correo <a href=\"mailto:administrador@buscoridecr.com?subjet:Problemas\">administrador@buscoridecr.com</a>" +
                                 "<br />" +
                                 "<br />" +
                                 "<small class=\"pull-right\">Aplican Restricciones, promoción válida hasta agotar existencias</small>" +
                                 "</div>" +
                                 "</div>" +
                                 "</div>";

                var emailsSent = 0;

                foreach (var user in users)
                {
                    EmailHandler.SendEmail(new IdentityMessage
                    {
                        Destination = user.Email,
                        Subject = "¡Correo Informativo buscoridecr.com!",
                        Body = message
                    }, EmailType.Notifications, false, logo);

                    emailsSent++;
                }

                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Info,
                    Message = "Correos enviados satisfactoriamente! Total: " + emailsSent + ". Correos pendientes de enviar en las próximas horas: " + _RemainingEmailsToSend.Count(),
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = "",
                    Fields = string.Empty
                }, logo);

                ViewBag.success = "¡Correos enviados satisfactoriamente!";
            }
            catch (Exception ex)
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = "Enviando correos. Envío cancelado. Error: " + ex.Message,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = "",
                    Fields = string.Empty
                }, logo);

                ViewBag.Error = "¡Hubo un error cargado la imagen, vualva a intentar!";
            }
        }
    }
}