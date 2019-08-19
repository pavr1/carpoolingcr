using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class VehiclesController : Controller
    {
        // GET: Vehicles
        public ActionResult Index()
        {
            return View();
        }
    }
}