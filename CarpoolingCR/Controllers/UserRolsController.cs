using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class UserRolsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ValidateUserRol()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = db.Users.Where(x => x.Email == User.Identity.Name).Single();

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
                    Timestamp = DateTime.Now,
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "Error inesperado, intente de nuevo!";

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
            }

            return View();
        }
    }
}