﻿using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!Common.IsAuthorized(User))
            {
                EmailHandler.SendEmailNewWebsiteHomeHit();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}