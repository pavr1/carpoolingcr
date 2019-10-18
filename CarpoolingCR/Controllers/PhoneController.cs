using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Data.Entity;
using System.Linq;
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
                        var phone = Request["phone"].Replace("-", string.Empty).Trim();

                        var existendPhone = db.Phones.Where(x => x.PhoneNumber == phone).SingleOrDefault();

                        if (existendPhone == null)
                        {
                            var t = string.Empty;
                            var sms = "Carpooling: Crea viajes, reserva espacios, recibe notificaciones, califica usuarios y mucho mas. Gratis, seguro y confiable.";//"CARPOOLING: Visita www.buscoridecr.com \nCreación de viajes, reservas, notificaciones, selección de asientos y mucho más.\n ¡Hagamos Ride!";
                            var msg = SMSHandler.SendSMS(phone, sms, "www.buscoridecr.com", logo, out t);

                            //¡Mensaje Enviado!
                            if (msg == "100075")
                            {
                                var phoneObj = new Phone { PhoneNumber = phone };

                                db.Entry(phoneObj).State = EntityState.Added;
                                db.SaveChanges();
                            }

                            return RedirectToAction("Index", new { message = msg, type = t });
                        }
                        else
                        {
                            Common.LogData(new Log
                            {
                                Line = Common.GetCurrentLine(),
                                Location = Enums.LogLocation.Server,
                                LogType = Enums.LogType.SMS,
                                Message = "El número ya fue notificado anteriormente",
                                Method = Common.GetCurrentMethod(),
                                Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                                UserEmail = User.Identity.Name
                            }, logo);

                            //¡Número anteriormente notificado!
                            return RedirectToAction("Index", new { message = "100076", type = "warn" });
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

                    //¡Error al mandar SMS!
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
    }
}