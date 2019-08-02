﻿using CarpoolingCR.Models;
using CarpoolingCR.Objects.Responses;
using CarpoolingCR.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Configuration;
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
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);
                var maxTripsPerUser = Convert.ToInt32(WebConfigurationManager.AppSettings["MaxTripsPerUser"]);
                var currentTrips = db.Trips.Where(x => x.ApplicationUserId == user.Id)
                    .Where(x => x.Status == Status.Activo)
                    .ToList();

                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

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
                var reachedMaxCount = false;

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

                        trip.DateTime = Common.ConvertToLocalTime(trip.DateTime);
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

                            trip.DateTime = Common.ConvertToLocalTime(trip.DateTime);
                        }

                        reachedMaxCount = (currentTrips.Count == maxTripsPerUser);
                    }
                }

                var response = new TripIndexResponse
                {
                    IsAdmin = isAdmin,
                    Trips = trips,
                    ReachedMaxCount = reachedMaxCount
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        public ActionResult DayTrips(string date, int from, int to)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                DateTime d = new DateTime();

                if (!DateTime.TryParse(date, out d))
                {
                    throw new Exception("Formato de fecha incorrecto. '" + date + "'");
                }

                var startDate = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
                var endDate = new DateTime(d.Year, d.Month, d.Day, 23, 59, 0);

                startDate = Common.ConvertToUTCTime(startDate);
                endDate = Common.ConvertToUTCTime(endDate);

                var result = db.Trips.Where(x => x.Status == Enums.Status.Activo
                    && x.DateTime >= startDate && x.DateTime <= endDate
                    && x.FromTown == from && x.ToTown == to)
                    .Include(x => x.ApplicationUser)
                    .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result[i].DateTime = Common.ConvertToLocalTime(result[i].DateTime);
                }

                var user = Common.GetUserByEmail(User.Identity.Name);

                var existentReservation = db.Reservations.Where(x => x.ApplicationUserId == user.Id)
                    .Where(x => x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending)
                    .ToList();

                return View(new TripDayTripsResponse
                {
                    Trips = result,
                    From = from,
                    To = to,
                    CurrentUserId = user.Id,
                    CurrentDate = d,
                    ExistentReservations = existentReservation
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        public ActionResult TripDetail(int id)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var trip = db.Trips.Where(x => x.TripId == id)
                    .Include(x => x.ApplicationUser)
                    .Single();

                trip.DateTime = Common.ConvertToLocalTime(trip.DateTime);

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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            try
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

                trip.DateTime = Common.ConvertToLocalTime(trip.DateTime);

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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);

                var response = new TripCreateResponse
                {
                    Towns = Common.GetLocationsStrings((int)user.CountryId)
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

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
            var fields = "Fields => ";

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                #region Fields
                fields += "FromTown: " + Request["FromTown"];
                fields += "ToTown: " + Request["ToTown"] + ", ";
                fields += "AvailableSpaces: " + Request["AvailableSpaces"];
                fields += "DateTime: " + Request["DateTime"];
                fields += "Trip.Details: " + Request["Trip.Details"];
                fields += "Trip.Price: " + Request["Trip.Price"];
                fields += "TotalSpaces: " + Request["TotalSpaces"];
                #endregion

                if (ModelState.IsValid)
                {
                    var user = Common.GetUserByEmail(User.Identity.Name);
                    var fromRequest = Convert.ToInt32(Request["FromTown"]);
                    var from = db.Districts.Where(x => x.DistrictId == fromRequest).SingleOrDefault();
                    var tripDate = Convert.ToDateTime(Request["DateTime"]);

                    if (from == null)
                    {
                        //¡Origen no válido!
                        ViewBag.Warning = "10005";

                        var response = new TripCreateResponse
                        {
                            Towns = Common.GetLocationsStrings((int)user.CountryId),
                            Trip = trip
                        };

                        return View(response);
                    }

                    var toRequest = Convert.ToInt32(Request["ToTown"]);
                    var to = db.Districts.Where(x => x.DistrictId == toRequest).SingleOrDefault();

                    if (to == null)
                    {
                        //¡Destino no válido!
                        ViewBag.Warning = "10006";

                        var response = new TripCreateResponse
                        {
                            Towns = Common.GetLocationsStrings((int)user.CountryId),
                            Trip = trip
                        };

                        return View(response);
                    }

                    trip = new Trip
                    {
                        ApplicationUserId = user.Id,
                        AvailableSpaces = Convert.ToInt32(Request["AvailableSpaces"]),
                        CreatedTime = Common.ConvertToUTCTime(DateTime.Now),
                        DateTime = Common.ConvertToUTCTime(tripDate),
                        Details = Request["Trip.Details"],
                        FromTown = fromRequest,
                        Price = Convert.ToDecimal(Request["Trip.Price"]),
                        Status = Enums.Status.Activo,
                        TotalSpaces = Convert.ToInt32(Request["TotalSpaces"]),
                        ToTown = toRequest
                    };

                    db.Trips.Add(trip);
                    db.SaveChanges();

                    var tripInfo = trip.FromTown + " a " + trip.ToTown + " el " + Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt");

                    EmailHandler.SendEmailTripCreation(WebConfigurationManager.AppSettings["AdminEmails"], user.FullName, tripInfo, trip.AvailableSpaces);

                    new SignalHandler().SendMessage(Enums.EventTriggered.TripCreated.ToString(), "");

                    //¡Viaje Creado!
                    return RedirectToAction("Index", new { message = "10007", type = "info" });
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name,
                    Fields = fields
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            //try
            //{
            //    if (!Common.IsAuthorized(User))
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }

            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }

            //    var response = new TripEditResponse
            //    {
            //        Trip = db.Trips.Where(x => x.TripId == id)
            //        .Include(x => x.ApplicationUser)
            //        .Single(),
            //        Towns = db.Towns.ToList()
            //    };

            //    if (response.Trip == null)
            //    {
            //        return HttpNotFound();
            //    }

            //    response.Trip.DateTime = Common.ConvertToLocalTime(response.Trip.DateTime);
            //    //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", trip.JourneyId);
            //    return View(response);
            //}
            //catch (Exception ex)
            //{
            //    Common.LogData(new Log
            //    {
            //        Line = Common.GetCurrentLine(),
            //        Location = Enums.LogLocation.Server,
            //        LogType = Enums.LogType.Error,
            //        Message = ex.Message + " / " + ex.StackTrace,
            //        Method = Common.GetCurrentMethod(),
            //        Timestamp = Common.ConvertToUTCTime(DateTime.Now),
            //        UserEmail = User.Identity.Name
            //    });

            //    ViewBag.Error = "¡Error inesperado, intente de nuevo!";

            //    return View();
            //}
            return View();
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
                        CreatedTime = Common.ConvertToUTCTime(Convert.ToDateTime(Request["DateTime"])),
                        DateTime = Common.ConvertToUTCTime(Convert.ToDateTime(Request["DateTime"])),
                        Details = Request["Trip.Details"],
                        FromTown = Convert.ToInt32(Request["FromTown"]),
                        Price = Convert.ToDecimal(Request["Trip.Price"]),
                        Status = Enums.Status.Activo,
                        TotalSpaces = Convert.ToInt32(Request["TotalSpaces"]),
                        ToTown = Convert.ToInt32(Request["ToTown"])
                    };

                    db.Entry(trip).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { message = "", type = "info" });
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            try
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
            catch (Exception ex)
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

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
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

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
                    EmailHandler.SendTripsCancelledByDriver(passengersToNoticeEmail, trip.FromTown + " -> " + trip.ToTown, Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt"), string.Empty);
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

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
