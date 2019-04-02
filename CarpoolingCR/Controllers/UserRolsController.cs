﻿using CarpoolingCR.Models;
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

                ViewBag.Error = "Hubo un error inesperado, por favor intente de nuevo.";

                return View();
            }
        }

        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult DriverIndex()
        {
            return View();
        }

        public ActionResult PassengerIndex()
        {
            return View();
        }
    }
}