using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
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

                        //string path = relativePath;

                        var users = db.Users.Where(x => x.Status == Enums.ProfileStatus.Active).ToList();
                        
                        var sendEmails = new Thread(() => SendToAll(users, absolutePath));
                        sendEmails.Start();
                    }
                }

                //¡Procesando de envío a todos los usuarios!
                return RedirectToAction("Index", new { message = "100062" });
            }
            catch (Exception ex)
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / " + ex.StackTrace,
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
            foreach (var user in users)
            {
               EmailHandler.SendEmail(new IdentityMessage
                {
                    Destination = user.Email,
                    Subject = "¡Correo Informativo buscoridecr.com!",
                    Body = string.Empty
                }, EmailType.Notifications, false, logo);
            }
        }
    }
}