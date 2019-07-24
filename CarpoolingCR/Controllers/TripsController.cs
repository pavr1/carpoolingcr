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
    public class TripsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trips
        public ActionResult Index(string message, string type)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    if (type == "info")
                    {
                        ViewBag.Info = message;
                    }
                    else if (type == "error")
                    {
                        ViewBag.Error = message;
                    }
                    else if (type == "warning")
                    {
                        ViewBag.Warning = message;
                    }
                }

                List<Trip> trips = new List<Trip>();
                var isAdmin = false;

                if (Common.GetUserType(User.Identity.Name) == Enums.UserType.Administrador)
                {
                    isAdmin = true;

                    trips = db.Trips.Where(x => x.Status == Enums.Status.Activo)
                        .Include(x => x.ApplicationUser)
                        .ToList();

                    foreach (var trip in trips)
                    {
                        trip.Reservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                            .Where(x => x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending)
                            .Include(x => x.ApplicationUser)
                            .ToList();

                        trip.DateTime = trip.ConvertToLocalTime(trip.DateTime);
                    }
                }
                else
                {
                    if (Common.GetUserType(User.Identity.Name) == Enums.UserType.Conductor)
                    {
                        trips = db.Trips.Where(x => x.ApplicationUser.Email == User.Identity.Name)
                            .Where(x => x.Status == Status.Activo || x.Status == Status.Pendiente)
                            .Include(x => x.ApplicationUser)
                            .ToList();

                        foreach (var trip in trips)
                        {
                            trip.Reservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                                .Where(x => x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending)
                                .Include(x => x.ApplicationUser)
                                .ToList();

                            trip.DateTime = trip.ConvertToLocalTime(trip.DateTime);
                        }
                    }
                }

                var response = new TripIndexResponse
                {
                    IsAdmin = isAdmin,
                    Trips = trips
                };

                return View(response);
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

        public ActionResult DayTrips(string date, string from, string to)
        {
            try
            {
                DateTime d = new DateTime();

                if (!DateTime.TryParse(date, out d))
                {
                    throw new Exception("Formato de fecha incorrecto. '" + date + "'");
                }

                var startDate = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
                var endDate = new DateTime(d.Year, d.Month, d.Day, 23, 59, 0);

                var result = db.Trips.Where(x => x.Status == Enums.Status.Activo
                    && x.DateTime >= startDate && x.DateTime <= endDate
                    && x.FromTown == from && x.ToTown == to)
                    .Include(x => x.ApplicationUser)
                    .ToList();

                foreach (var trip in result)
                {
                    trip.DateTime = trip.ConvertToLocalTime(trip.DateTime);
                }

                return View(new TripDayTripsResponse
                {
                    Trips = result,
                    From = from,
                    To = to,
                    CurrentDate = d
                });
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

        public ActionResult TripDetail(int id)
        {
            try
            {
                var trip = db.Trips.Where(x => x.TripId == id)
                    .Include(x => x.ApplicationUser)
                    .Single();

                trip.DateTime = trip.ConvertToLocalTime(trip.DateTime);

                return View(trip);
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

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Trip trip = db.Trips.Find(id);
                if (trip == null)
                {
                    return HttpNotFound();
                }

                trip.DateTime = trip.ConvertToLocalTime(trip.DateTime);

                return View(trip);
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

        // GET: Trips/Create
        public ActionResult Create()
        {
            try
            {
                var user = Common.GetUserByEmail(User.Identity.Name);

                var response = new TripCreateResponse
                {
                    Towns = db.Towns.Where(x => x.Status == Enums.TownStatus.Active && x.CountryId == user.CountryId).ToList()
                };

                return View(response);
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

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "")] Trip trip)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = Common.GetUserByEmail(User.Identity.Name);
                    var fromRequest = Request["FromTown"];
                    var from = db.Towns.Where(x => x.Name == fromRequest).SingleOrDefault();

                    if (from == null)
                    {
                        ViewBag.Warning = "Origen no valido.";

                        var response = new TripCreateResponse
                        {
                            Towns = db.Towns.Where(x => x.Status == Enums.TownStatus.Active && x.CountryId == user.CountryId).ToList(),
                            Trip = trip
                        };

                        return View(response);
                    }

                    var toRequest = Request["ToTown"];
                    var to = db.Towns.Where(x => x.Name == toRequest).SingleOrDefault();

                    if (to == null)
                    {
                        ViewBag.Warning = "Destino no valido.";

                        var response = new TripCreateResponse
                        {
                            Towns = db.Towns.Where(x => x.Status == Enums.TownStatus.Active && x.CountryId == user.CountryId).ToList(),
                            Trip = trip
                        };

                        return View(response);
                    }

                    if (fromRequest.ToUpper() == toRequest.ToUpper())
                    {
                        ViewBag.Warning = "El origen y destino no pueden ser iguales.";

                        var response = new TripCreateResponse
                        {
                            Towns = db.Towns.Where(x => x.Status == Enums.TownStatus.Active && x.CountryId == user.CountryId).ToList(),
                            Trip = trip
                        };

                        return View(response);
                    }

                    trip = new Trip
                    {
                        ApplicationUserId = user.Id,
                        AvailableSpaces = Convert.ToInt32(Request["AvailableSpaces"]),
                        CreatedTime = DateTime.Now,
                        DateTime = trip.ConvertToUTCTime(Convert.ToDateTime(Request["DateTime"])),
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

                    return RedirectToAction("Index", new { message = "Viaje Creado!", type = "info" });
                }

                //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", trip.JourneyId);
                return View();
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

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
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

                response.Trip.DateTime = response.Trip.ConvertToLocalTime(response.Trip.DateTime);
                //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", trip.JourneyId);
                return View(response);
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

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "")] Trip trip)
        {
            try
            {
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

                    trip.DateTime = trip.ConvertToUTCTime(trip.DateTime);

                    db.Entry(trip).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { message = "Viaje Actualizado!", type = "info" });
                }
                //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", trip.JourneyId);
                return View(trip);
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

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
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

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tran = db.Database.BeginTransaction();

            try
            {
                id = Convert.ToInt32(Request["tripId"]);

                Trip trip = db.Trips.Find(id);

                if (trip == null)
                {
                    return RedirectToAction("Index", new { message = "Viaje no encontrado!", type = "warning" });
                }

                trip.Reservations = db.Reservations.Where(x => x.TripId == id)
                    .Where(x => x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending)
                    .Include(x => x.ApplicationUser)
                    .ToList();

                var passengersToNoticeEmail = string.Empty;

                foreach (var reservation in trip.Reservations)
                {
                    reservation.Status = ReservationStatus.Cancelled;
                    db.Entry(reservation).State = EntityState.Modified;
                    db.SaveChanges();

                    passengersToNoticeEmail += reservation.ApplicationUser.Email + ",";
                }

                if (passengersToNoticeEmail.Length > 0)
                {
                    passengersToNoticeEmail = passengersToNoticeEmail.Substring(0, passengersToNoticeEmail.Length - 1);
                }

                trip.Status = Status.Cancelado;

                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();

                //new SignalHandler().SendMessage(Enums.EventTriggered.TripDeleted.ToString(), "");

                if (passengersToNoticeEmail.Length > 0)
                {
                    EmailHandler.SendTripsCancelledByDriver(passengersToNoticeEmail, trip.FromTown + " -> " + trip.ToTown, trip.DateTime.ToString(), string.Empty);
                }

                tran.Commit();

                return RedirectToAction("Index", new { message = "Viaje Eliminado!", type = "info" });
            }
            catch (Exception ex)
            {
                tran.Rollback();

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
