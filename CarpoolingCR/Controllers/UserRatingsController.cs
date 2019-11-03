using CarpoolingCR.Models;
using CarpoolingCR.Objects.Responses;
using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Controllers
{
    public class UserRatingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserRatings
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //if trip is not null then it means it's the driver qualifying passenger
        public string RateUser(int tripId, string from, string to, int stars, string comment, bool isPassenger)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            var tran = db.Database.BeginTransaction();

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);

                var rate = new UserRating
                {
                    TripId = tripId,
                    FromId = from,
                    ToId = to,
                    Stars = stars,
                    Comments = comment
                };

                db.Entry(rate).State = EntityState.Added;
                db.SaveChanges();

                var html = string.Empty;

                if (isPassenger)
                {
                    var currentUTCTime = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime());

                    var reservations = db.Reservations.Where(x => x.ApplicationUserId == user.Id)
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Trip)
                    .Where(x => x.Trip.DateTime <= currentUTCTime)
                    .ToList();

                    foreach (var res in reservations)
                    {
                        res.Trip = db.Trips.Where(x => x.TripId == res.TripId)
                            .Include(x => x.ApplicationUser)
                            .Single();

                        res.Trip.UserRatings = db.UserRatings.Where(x => x.TripId == res.TripId).ToList();
                        res.Trip.FromTown = db.Districts.Where(x => x.DistrictId == res.Trip.FromTownId).Single();
                        res.Trip.ToTown = db.Districts.Where(x => x.DistrictId == res.Trip.ToTownId).Single();
                        res.Trip.Route = db.Districts.Where(x => x.DistrictId == res.Trip.RouteId).Single();
                        res.Trip.UserRatings = db.UserRatings.Where(x => x.TripId == res.TripId).ToList();

                        //res.Trip.Qualifications = db.Qualifications.Where(x => x.TripId == res.TripId && x.QualifierId != user.Id)
                        //    .Include(x => x.Qualifier)
                        //    .ToList();

                        if (res.Trip.Reservations != null)
                        {
                            foreach (var res1 in res.Trip.Reservations)
                            {
                                res1.Trip = null;
                            }
                        }
                    }

                    var response = new HistorialResponse
                    {
                        Reservations = reservations,
                        //¡Usuario Calificado!
                        Message = "100074",
                        MessageType = "success"
                    };

                    html = Serializer.RenderViewToString(this.ControllerContext, "../Reservations/Partials/_PassengerReservationHistorial", response);
                    response.Html = html;

                    html = Serializer.Serialize(response);

                    tran.Commit();

                    return html;
                }
                else
                {
                    List<Trip> trips = new List<Trip>();

                    if (user.UserType == UserType.Administrador)
                    {
                        trips = db.Trips.Where(x => x.Status == Status.Finalizado)
                        .ToList();
                    }
                    else if (user.UserType == UserType.Conductor)
                    {
                        trips = db.Trips.Where(x => x.ApplicationUserId == user.Id)
                        .Where(x => x.Status == Status.Finalizado)
                        .ToList();
                    }

                    foreach (var trip in trips)
                    {
                        trip.Reservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                            .Include(x => x.ApplicationUser)
                            .ToList();

                        trip.UserRatings = db.UserRatings.Where(x => x.TripId == trip.TripId).ToList();

                        foreach (var res in trip.Reservations)
                        {
                            res.Trip = null;
                        }

                        trip.ApplicationUser = db.Users.Where(x => x.Id == trip.ApplicationUserId).Single();
                        trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                        trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();
                        trip.Route = db.Districts.Where(x => x.DistrictId == trip.RouteId).Single();
                    }

                    trips.Sort((x, y) => y.CreatedTime.CompareTo(x.CreatedTime));

                    var response = new HistorialResponse
                    {
                        Trips = trips,
                        //¡Usuario Calificado!
                        Message = "100074",
                        MessageType = "success"
                    };

                    html = Serializer.RenderViewToString(this.ControllerContext, "../Trips/Partials/_DriverTripHistorial", response);
                    response.Html = html;

                    html = Serializer.Serialize(response);

                    tran.Commit();

                    return html;
                }
            }
            catch (Exception ex)
            {
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

                tran.Rollback();

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

                return string.Empty;
            }
        }

        // GET: Qualifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Qualification qualification = db.Qualifications.Find(id);
            if (qualification == null)
            {
                return HttpNotFound();
            }
            return View(qualification);
        }

        // GET: Qualifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Qualifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QualificationId,FromUserId,ToUserId,Starts,Comments")] Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                db.Qualifications.Add(qualification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qualification);
        }

        // GET: Qualifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Qualification qualification = db.Qualifications.Find(id);
            if (qualification == null)
            {
                return HttpNotFound();
            }
            return View(qualification);
        }

        // POST: Qualifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QualificationId,FromUserId,ToUserId,Starts,Comments")] Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qualification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qualification);
        }

        // GET: Qualifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Qualification qualification = db.Qualifications.Find(id);
            if (qualification == null)
            {
                return HttpNotFound();
            }
            return View(qualification);
        }

        // POST: Qualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Qualification qualification = db.Qualifications.Find(id);
            db.Qualifications.Remove(qualification);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
