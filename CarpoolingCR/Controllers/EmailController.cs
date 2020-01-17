using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fnSplit = Path.GetFileName(file.FileName).Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                        var fileName = "NotificationEmail_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss") + "." + fnSplit[1];

                        string relativePath = "~/Content/Pictures/GroupEmails/" + fileName;
                        string absolutePath = Server.MapPath(relativePath);

                        try
                        {
                            file.SaveAs(absolutePath);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error saving image. Relative Path: " + relativePath + ". Absolute Path: " + absolutePath + ". Error: " + ex.Message);
                        }

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

                        var sendEmails = new Thread(() => SendToAll(users, absolutePath));
                        sendEmails.Start();
                    }
                }
                else
                {
                    Common.LogData(new Log
                    {
                        Line = Common.GetCurrentLine(),
                        Location = Enums.LogLocation.Server,
                        LogType = Enums.LogType.Error,
                        Message = "Attaching file not found!",
                        Method = Common.GetCurrentMethod(),
                        Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                        UserEmail = User.Identity.Name,
                        Fields = string.Empty
                    }, logo);
                }

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

        private void SendToAll(List<ApplicationUser> users, string logo)
        {
            try
            {
                string message = "<div class=\"panel panel-default\" style=\"text-align:justify; border-radius:10px; padding:20px; margin:20px\">" +
                                    "<h4>" +
                                        "Promoción de principio de año 2020" +
                                    "</h4>" +
                                    "<hr/>" +
                                    "¡Gánate ₡1000,00 por cada usuario que se registre en www.buscoridecr.com por medio de tu enlace de referencia!" +
                                    "<br/>" +
                                    "<br/>" +
                                    "Participa en esta promoción de principio de año y gana dinero por cada usuario que se registre por medio de tu enlace." +
                                    "<br/>" +
                                    "<br/>" +
                                    "¿Cómo Funciona?" +
                                    "<br/>" +
                                    "Busca el \"Enlace de Referencia\" desde tu <a href = \"~/Manage/ProfileInfo\" > Perfíl </a>, da click en \"Copiar\" y envíalo a todos tus contactos por los diferentes canales de comunicación (Whatsapp, Messenger, E-mail, etc)." +
                                    "<br/>" +
                                    "<br/>" +
                                    "Por cada usuario que se registre desde tu enlace de referencia, se te acreditarán ₡1000,00 a tu monedero virtual, los cuales podrás utilizar para pagos totales, parciales en tus viajes o retiros." +
                                    "<br/>" +
                                    "<br/>" +
                                    "¡Los usuarios nuevos también ganan! Al abrir una nueva cuenta con www.buscoridecr.com recibirán ₡1000,00." +
                                    "<br/>" +
                                    "<br/>" +
                                    "<small class=\"pull-right\">Aplican Restricciones, promoción válida hasta agotar existencias</small>" +
                                    "</div>";

                foreach (var user in users)
                {
                    EmailHandler.SendEmail(new IdentityMessage
                    {
                        Destination = user.Email,
                        Subject = "¡Correo Informativo buscoridecr.com!",
                        Body = message
                    }, EmailType.Notifications, false, logo);
                }

                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Info,
                    Message = "Correos enviados satisfactoriamente!",
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