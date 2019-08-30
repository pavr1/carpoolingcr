using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var user = Common.GetUserByEmail(User.Identity.Name);

            if (!Common.IsAuthorized(User))
            {
                var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                if (send)
                {
                    EmailHandler.HomePageHit();
                }
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