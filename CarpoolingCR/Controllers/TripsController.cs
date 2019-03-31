using CarpoolingCR.Models;
using CarpoolingCR.Objects.Responses;
using CarpoolingCR.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class TripsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trips
        public ActionResult Index()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            List<Trip> trips = new List<Trip>();

            if (Common.GetUserType(User.Identity.Name) == Enums.UserType.Administrador)
            {
                trips = db.Trips.Where(x => x.Status == Enums.Status.Activo).ToList();
            }
            else
            {
                if (Common.GetUserType(User.Identity.Name) == Enums.UserType.Conductor)
                {
                    trips = db.Trips.Where(x => x.Status == Enums.Status.Activo && x.ApplicationUser.Email == User.Identity.Name)
                        .Include(x => x.ApplicationUser)
                        .ToList();
                }
            }

            var response = new TripIndexResponse
            {
                Trips = trips
            };

            return View(response);
        }

        public ActionResult DayTrips(string date, string from, string to)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            DateTime d = new DateTime();

            if (!DateTime.TryParse(date, out d))
            {

            }

            var startDate = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
            var endDate = new DateTime(d.Year, d.Month, d.Day, 23, 59, 0);

            var result = db.Trips.Where(x => x.Status == Enums.Status.Activo
                && x.DateTime >= startDate && x.DateTime <= endDate
                && x.FromTown == from && x.ToTown == to)
                .Include(x => x.ApplicationUser)
                .ToList();


            return View(new TripDayTripsResponse
            {
                Trips = result,
                From = from,
                To = to,
                CurrentDate = d
            });
        }

        public ActionResult TripDetail(int id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var trip = db.Trips.Where(x => x.TripId == id)
                .Include(x => x.ApplicationUser)
                .Single();

            return View(trip);
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = Common.GetUserByEmail(User.Identity.Name);

            var response = new TripCreateResponse
            {
                Towns = db.Towns.Where(x => x.Status == Enums.TownStatus.Active && x.CountryId == user.CountryId).ToList()
            };

            return View(response);
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "")] Trip trip)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                var currentUser = db.Users.Where(x => x.Email == User.Identity.Name).Single();

                trip = new Trip
                {
                    ApplicationUser = currentUser,
                    ApplicationUserId = currentUser.Id,
                    AvailableSpaces = Convert.ToInt32(Request["AvailableSpaces"]),
                    CreatedTime = DateTime.Now,
                    DateTime = Convert.ToDateTime(Request["DateTime"]),
                    Details = Request["Trip.Details"],
                    FromTown = Request["FromTown"],
                    Price = Convert.ToDecimal(Request["Trip.Price"]),
                    Status = Enums.Status.Activo,
                    TotalSpaces = Convert.ToInt32(Request["TotalSpaces"]),
                    ToTown = Request["ToTown"]
                };

                db.Trips.Add(trip);
                db.SaveChanges();

                new SignalHandler().SendMessage(Enums.EventTriggered.TripCreated.ToString(), "");

                return RedirectToAction("Index");
            }

            //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", trip.JourneyId);
            return View();
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = new TripEditResponse
            {
                Trip = db.Trips.Where(x => x.TripId == id)
                .Include(x => x.ApplicationUser)
                .Single(),
                Towns = db.Towns.ToList()
            };

            if (response.Trip == null)
            {
                return HttpNotFound();
            }
            //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", trip.JourneyId);
            return View(response);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "")] Trip trip)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            ModelState.Values.ToList()[4].Errors.Clear();

            if (ModelState.IsValid)
            {
                trip = new Trip
                {
                    TripId = Convert.ToInt32(Request["Trip.TripId"]),
                    ApplicationUserId = Request["Trip.ApplicationUserId"],
                    AvailableSpaces = Convert.ToInt32(Request["AvailableSpaces"]),
                    CreatedTime = Convert.ToDateTime(Request["DateTime"]),
                    DateTime = Convert.ToDateTime(Request["DateTime"]),
                    Details = Request["Trip.Details"],
                    FromTown = Request["FromTown"],
                    Price = Convert.ToDecimal(Request["Trip.Price"]),
                    Status = Enums.Status.Activo,
                    TotalSpaces = Convert.ToInt32(Request["TotalSpaces"]),
                    ToTown = Request["ToTown"]
                };

                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", trip.JourneyId);
            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trip trip = db.Trips.Where(x => x.TripId == id)
                .Single();

            if (trip == null)
            {
                return HttpNotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            id = Convert.ToInt32(Request["tripId"]);

            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
            db.SaveChanges();

            new SignalHandler().SendMessage(Enums.EventTriggered.TripDeleted.ToString(), "");

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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
