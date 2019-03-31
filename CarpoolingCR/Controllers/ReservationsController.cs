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
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var response = new ReservationIndexResponse
            {
                Reservations = db.Reservations.ToList(),
                Towns = db.Towns.ToList()
            };

            return View(response);
        }

        public ActionResult Transportation()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var reservations = new List<Reservation>();
            var user = Common.GetUserByEmail(User.Identity.Name);
            var driverName = string.Empty;

            if (user.UserType == Enums.UserType.Pasajero)
            {
                reservations = db.Reservations.Where(x => x.ApplicationUser.Email == User.Identity.Name && (x.Status == Enums.Status.Activo.ToString() || x.Status == Enums.Status.Pendiente.ToString()))
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Trip)
                    .Include(x => x.Trip.ApplicationUser)
                    .ToList();
            }
            else if (user.UserType == Enums.UserType.Conductor)
            {
                var trips = db.Trips.Where(x => x.Status == Status.Activo && x.ApplicationUser.Email == User.Identity.Name).ToList();

                foreach (var trip in trips)
                {
                    var tripReservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                        .Include(x => x.ApplicationUser)
                        .Include(x => x.Trip)
                        .Include(x => x.Trip.ApplicationUser)
                        .ToList();

                    reservations.AddRange(tripReservations);
                }
            }
            else if (user.UserType == Enums.UserType.Administrador)
            {
                var trips = db.Trips.Where(x => x.Status == Status.Activo).ToList();

                foreach (var trip in trips)
                {
                    var tripReservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                        .Include(x => x.ApplicationUser)
                        .Include(x => x.Trip)
                        .Include(x => x.Trip.ApplicationUser)
                        .ToList();

                    reservations.AddRange(tripReservations);
                }
            }

            ReservationTransportationResponse response = new ReservationTransportationResponse
            {
                Trips = new List<Trip>(),
                //JourneyList = new SelectList(journeys, "Key", "Value"),
                PendingReservations = reservations,
                SelectedJourneyId = -1,
                SelectedRouteIndex = -1,
                CurrentUserType = user.UserType,
                Towns = db.Towns.ToList()
            };

            return View(response);
        }

        public ActionResult ChatTest()
        {
            return View();
        }

        [HttpPost]
        public string Transportation(string from, string to)
        {
            if (!Common.IsAuthorized(User))
            {
                RedirectToAction("Login", "Account");
            }

            //var journeys = db.Journeys.Where(x => x.Status == Enums.Status.Activo).Select(s => new { s.JourneyId, s.Name }).ToDictionary(d => d.JourneyId, d => d.Name);
            List<Trip> trips = db.Trips.Where(x => x.FromTown == from && x.ToTown == to && x.Status == Status.Activo).ToList();
            List<Reservation> reservations = new List<Reservation>();

            var user = Common.GetUserByEmail(User.Identity.Name);

            if (user.UserType == UserType.Administrador)
            {
                reservations = db.Reservations.Where(x => x.Status == Enums.Status.Activo.ToString() || x.Status == Enums.Status.Pendiente.ToString())
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Trip)
                    .Include(x => x.Trip.ApplicationUser)
                    .ToList();
            }
            else if (user.UserType == UserType.Conductor)
            {
                reservations = db.Reservations.Where(x => x.Trip.ApplicationUser.Email == User.Identity.Name)
                   .Include(x => x.ApplicationUser)
                   .Include(x => x.Trip)
                   .Include(x => x.Trip.ApplicationUser)
                   .ToList();
            }
            else if (user.UserType == UserType.Pasajero)
            {
                reservations = db.Reservations.Where(x => x.ApplicationUser.Email == User.Identity.Name)
                   .Include(x => x.ApplicationUser)
                   .Include(x => x.Trip)
                   .Include(x => x.Trip.ApplicationUser)
                   .ToList();
            }

            ReservationTransportationResponse response = new ReservationTransportationResponse
            {
                Trips = trips,
                //JourneyList = new SelectList(journeys, "Key", "Value"),
                PendingReservations = reservations,
                //SelectedJourneyId = journeyId,
                //SelectedRouteIndex = routeDetailIndex,
                Towns = db.Towns.ToList()
            };

            response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

            return Serializer.Serialize(response);
        }

        // GET: Reservations/Details/5
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
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        [HttpGet]
        public ActionResult Create(int tripId)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var trip = db.Trips.Where(x => x.TripId == tripId)
                .Include(x => x.ApplicationUser)
                .SingleOrDefault();

            var response = new ReservationCreateResponse
            {
                Trip = trip
            };

            return View(response);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var passenger = Common.GetUserByEmail(User.Identity.Name);

            Reservation reservation = new Reservation
            {
                ApplicationUserId = passenger.Id,
                Date = DateTime.Now,
                PassengerName = passenger.Name + " " + passenger.LastName + " " + passenger.SecondLastName,
                RequestedSpaces = Convert.ToInt32(Request["RequestedSpaces"]),
                Status = Enums.Status.Pendiente.ToString(),
                TripId = Convert.ToInt32(Request["TripId"])
            };

            var trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                .Include(x => x.ApplicationUser)
                .Single();

            reservation.ApplicationUserId = passenger.Id;
            reservation.Date = DateTime.Now;
            reservation.Status = Enums.Status.Pendiente.ToString();

            db.Reservations.Add(reservation);
            db.SaveChanges();

            var passengerName = passenger.Name + " " + passenger.LastName;
            var tripInfo = trip.FromTown + " a " + trip.ToTown + " el " + trip.DateTime.ToString();
            var spaces = reservation.RequestedSpaces;

            var html = "<html><header></header><body>" + passengerName + " ha solicitado " + spaces + " espacios para tu viaje de " + tripInfo + "<br/><br/>Da click <b><a href='" + "callbackUrl" + "'>aquí</a></b> para ver la reserva!</body></html>";

            Common.SendEmail(new IdentityMessage
            {
                Destination = trip.ApplicationUser.Email,
                Subject = "Han solicitado espacio en tu vehículo!",
                Body = html
            });

            return RedirectToAction("Transportation", "Reservations");

        }

        // GET: Reservations/Edit/5
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
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,TripId,UserEmail,RequestedSpaces,Date,Status")] Reservation reservation)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
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
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Transportation");
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
