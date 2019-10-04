using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Controllers
{
    public class RideRequestsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: RideRequests
        //public ActionResult Index()
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        //var rideRequests = db.RideRequests.Include(r => r.Journey);
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// GET: RideRequests/Details/5
        //public ActionResult Details(int? id)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        RideRequest rideRequest = db.RideRequests.Find(id);
        //        if (rideRequest == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(rideRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// GET: RideRequests/Create
        //public ActionResult Create()
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name");
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// POST: RideRequests/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "RideRequestId,UserId,JourneyId,RouteDetails,Date,IsTimeSpecific,DayPart,RequestedSpaces,Status")] RideRequest rideRequest)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            rideRequest.UserEmail = User.Identity.Name;
        //            rideRequest.Status = Status.Pendiente.ToString();

        //            db.RideRequests.Add(rideRequest);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", rideRequest.JourneyId);
        //        return View(rideRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// GET: RideRequests/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        RideRequest rideRequest = db.RideRequests.Find(id);
        //        if (rideRequest == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", rideRequest.JourneyId);
        //        return View(rideRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// POST: RideRequests/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "RideRequestId,UserId,JourneyId,RouteDetails,Date,IsTimeSpecific,DayPart,RequestedSpaces,Status")] RideRequest rideRequest)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(rideRequest).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", rideRequest.JourneyId);
        //        return View(rideRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// GET: RideRequests/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        RideRequest rideRequest = db.RideRequests.Find(id);
        //        if (rideRequest == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(rideRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// POST: RideRequests/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        RideRequest rideRequest = db.RideRequests.Find(id);
        //        db.RideRequests.Remove(rideRequest);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
