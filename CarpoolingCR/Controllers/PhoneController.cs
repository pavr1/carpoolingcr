using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class PhoneController : Controller
    {
        // GET: Phone
        public ActionResult Index(string message, string type)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (type == "success")
                {
                    ViewBag.Info = message;
                }
                else if (type == "warn")
                {
                    ViewBag.Warning = message;
                }
                else if (type == "error")
                {
                    ViewBag.Error = message;
                }
            }

            var user = Common.GetUserByEmail(User.Identity.Name);
            Common.UpdateMenuItemsCount(user.Id);

            return View();
        }

        [HttpPost]
        public ActionResult SendPromotionSMS()
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            try
            {
                var smsServiceEnabled = Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableSMS"]);

                if (smsServiceEnabled)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        var message = Request["message"];
                        var initialMessage = Request["ckInitialMessage"] == "on";

                        if (initialMessage)
                        {
                            var phone = Request["phone"].Replace("-", string.Empty).Trim();
                            var existendPhone = db.Phones.Where(x => x.PhoneNumber == phone).SingleOrDefault();

                            if (existendPhone == null)
                            {
                                var t = string.Empty;
                                var sms = message;//"Carpooling: Crea viajes, reserva espacios, recibe notificaciones, califica usuarios y mucho mas. Gratis, seguro y confiable.";
                                var msg = SMSHandler.SendSMS(phone, sms, "www.buscoridecr.com", logo, out t);

                                //¡Mensaje Enviado!
                                if (msg == "100026")
                                {
                                    var phoneObj = new Phone { PhoneNumber = phone };

                                    db.Entry(phoneObj).State = EntityState.Added;
                                    db.SaveChanges();
                                }

                                return RedirectToAction("Index", new { message = msg, type = t });
                            }
                            else
                            {
                                //¡Número anteriormente notificado!
                                return RedirectToAction("Index", new { message = "100076", type = "warn" });
                            }
                        }
                        else
                        {
                            //send to all numbers
                            var users = db.Users.Where(x => x.Status == Enums.ProfileStatus.Active).ToList();

                            var sendEmails = new Thread(() => SendSMSToUsers(users, message, logo));
                            sendEmails.Start();

                            //¡Enviando mensajes, revisar logs en un momento!
                            return RedirectToAction("Index", new { message = "100091", type = "success" });
                        }
                    }
                }
                else
                {
                    Common.LogData(new Log
                    {
                        Line = Common.GetCurrentLine(),
                        Location = Enums.LogLocation.Server,
                        LogType = Enums.LogType.SMS,
                        Message = "Envío de mensaje SMS inactivo",
                        Method = Common.GetCurrentMethod(),
                        Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                        UserEmail = User.Identity.Name
                    }, logo);

                    //¡Error al mandar mensaje, SMS inactivo!
                    return RedirectToAction("Index", new { message = "100077", type = "error" });
                }
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
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return RedirectToAction("Index", new { message = "" });
            }
        }

        private void SendSMSToUsers(List<ApplicationUser> users, string message, string logo)
        {
            var sentToAll = true;
            var notSentMsgsCodes = string.Empty;

            foreach (var user in users)
            {
                var t = string.Empty;
                var sms = message;//"Carpooling: Crea viajes, reserva espacios, recibe notificaciones, califica usuarios y mucho mas. Gratis, seguro y confiable.";
                var msg = SMSHandler.SendSMS(user.Phone1, sms, "www.buscoridecr.com", logo, out t);

                //¡Mensaje Enviado!
                if (msg != "100026")
                {
                    sentToAll = false;
                    notSentMsgsCodes += msg + " | ";
                }
            }

            if (!sentToAll)
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.SMS,
                    Message = "No todos los mensajes fueron enviados: " + notSentMsgsCodes,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = "administrador"
                }, logo);
            }
            else
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.SMS,
                    Message = "Todos los mensajes fueron enviados",
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = "administrador"
                }, logo);
            }
        }
    }
}