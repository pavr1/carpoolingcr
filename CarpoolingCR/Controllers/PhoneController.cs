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
                    ViewBag.Success = message;
                }
                else if (type == "warn")
                {
                    ViewBag.Warning = message;
                }
                else if (type == "error")
                {
                    ViewBag.error = message;
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
                        var phone = Request["phone"];

                        var existendPhone = db.Phones.Where(x => x.PhoneNumber == phone).SingleOrDefault();

                        if (existendPhone == null)
                        {
                            SMSHandler.SendSMS(phone, "¿Carpooling?¡Visita www.buscoridecr.com!Creación de viajes, reservas, notificaciones, selección de asientos y mucho más.¡Hagamos Ride!", logo);

                            var phoneObj = new Phone { PhoneNumber = phone };

                            db.Entry(phoneObj).State = EntityState.Added;
                            db.SaveChanges();

                            return RedirectToAction("Index", new { message = "¡Mensaje Enviado!", type = "success" });
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

                            return RedirectToAction("Index", new { message = "¡Número ya notificado!", type = "warn" });
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

                    return RedirectToAction("Index", new { message = "¡Hubo un error!", type = "error" });
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