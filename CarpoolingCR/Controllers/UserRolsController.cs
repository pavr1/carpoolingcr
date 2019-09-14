using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarpoolingCR.Controllers
{
    public class UserRolsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ValidateUserRol()
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");;

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = db.Users.Where(x => x.Email == User.Identity.Name).SingleOrDefault();

                if (user != null)
                {
                    switch (user.UserType)
                    {
                        case Enums.UserType.Administrador:
                            return RedirectToAction("AdminIndex", "UserRols");
                        case Enums.UserType.Conductor:
                            return RedirectToAction("DriverIndex", "UserRols");
                        case Enums.UserType.Pasajero:
                            return RedirectToAction("PassengerIndex", "UserRols");
                    }
                }
                else
                {
                    //if could not retrieve user from db, then kill session and return to login
                    var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                    AuthenticationManager.SignOut();

                    return RedirectToAction("Login", "Account");
                }

                return RedirectToAction("AdminIndex", "UserRols");
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        public ActionResult AdminIndex()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = db.Users.Where(x => x.Email == User.Identity.Name).Single();

            if (user != null)
            {
               if(user.UserType != Enums.UserType.Administrador)
                {
                    return ValidateUserRol();
                }

                if (!user.IsPhoneVerified)
                {
                    return RedirectToAction("PhoneNumberNotVerified", "UserRols");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


            return View();
        }

        public ActionResult DriverIndex()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = db.Users.Where(x => x.Email == User.Identity.Name).Single();

            if (user != null)
            {
                if (user.UserType != Enums.UserType.Conductor)
                {
                    return ValidateUserRol();
                }

                if (!user.IsPhoneVerified)
                {
                    return RedirectToAction("PhoneNumberNotVerified", "UserRols");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult PassengerIndex()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = db.Users.Where(x => x.Email == User.Identity.Name).Single();

            if (user != null)
            {
                if (user.UserType != Enums.UserType.Pasajero)
                {
                    return ValidateUserRol();
                }

                if (!user.IsPhoneVerified)
                {
                    return RedirectToAction("PhoneNumberNotVerified", "UserRols");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult PhoneNumberNotVerified()
        {
            return View();
        }
    }
}