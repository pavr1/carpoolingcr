using CarpoolingCR.Models;
using CarpoolingCR.Models.Locations;
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

                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //sets expired trips' status "Finalizado"
                Common.FinilizeExpiredTrips();
                
                var maxTripsPerUser = Convert.ToInt32(WebConfigurationManager.AppSettings["MaxTripsPerUser"]);
                var currentTrips = db.Trips.Where(x => x.ApplicationUserId == user.Id)
                    .Where(x => x.Status == Status.Activo)
                    .ToList();

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

                        trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId)
                            .Include(x => x.County)
                            .Single();
                        trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId)
                            .Include(x => x.County)
                            .Single();

                        trip.Route = db.Districts.Where(x => x.DistrictId == trip.RouteId)
                            .Include(x => x.County)
                            .SingleOrDefault();

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

                            trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId)
                                .Include(x => x.County)
                                .Single();
                            trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId)
                                .Include(x => x.County)
                                .Single();
                            trip.Route = db.Districts.Where(x => x.DistrictId == trip.RouteId)
                            .Include(x => x.County)
                            .SingleOrDefault();

                            trip.DateTime = Common.ConvertToLocalTime(trip.DateTime);
                        }

                        reachedMaxCount = (currentTrips.Count == maxTripsPerUser);
                    }
                }

                var response = new TripIndexResponse
                {
                    IsAdmin = isAdmin,
                    Trips = trips,
                    ReachedMaxCount = reachedMaxCount,
                    VehicleInfoRegistered = (user.Vehicle != null)
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

        public ActionResult DayTrips(string date, string from, string to)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);
                var fromDistrict = new District();
                var toDistrict = new District();

                fromDistrict = Common.ValidateDistrictString(from);
                toDistrict = Common.ValidateDistrictString(to);

                DateTime d = new DateTime();

                if (!DateTime.TryParse(date, out d))
                {
                    throw new Exception("Formato de fecha incorrecto. '" + date + "'");
                }

                var startDate = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
                var endDate = new DateTime(d.Year, d.Month, d.Day, 23, 59, 0);

                startDate = Common.ConvertToUTCTime(startDate);
                endDate = Common.ConvertToUTCTime(endDate);

                var trips = db.Trips.Where(x => x.Status == Enums.Status.Activo
                    && x.DateTime >= startDate && x.DateTime <= endDate
                    && x.FromTownId == fromDistrict.DistrictId && x.ToTownId == toDistrict.DistrictId)
                    .Include(x => x.ApplicationUser)
                    .ToList();

                var couldNotFindExactTrip = false;
                Common.GetNearByTripsForReservationTransportation(fromDistrict, toDistrict, user, ref trips, startDate, endDate, out couldNotFindExactTrip);

                for (int i = 0; i < trips.Count; i++)
                {
                    var fromId = trips[i].FromTownId;
                    var toId = trips[i].ToTownId;
                    var routeId = trips[i].RouteId;
                    trips[i].FromTown = db.Districts.Where(x => x.DistrictId == fromId).Single();
                    trips[i].ToTown = db.Districts.Where(x => x.DistrictId == toId).Single();
                    trips[i].Route = db.Districts.Where(x => x.DistrictId == routeId).Single();
                    trips[i].DateTime = Common.ConvertToLocalTime(trips[i].DateTime);
                    // \\ chars giving errors when parsing to Json, so replace them to parse correctly and change back in javascript
                    trips[i].ApplicationUser.Picture = trips[i].ApplicationUser.Picture.Replace("/", "|");
                    trips[i].FromTown.County = null;
                    trips[i].ToTown.County = null;
                    trips[i].Route.County = null;
                }

                var existentReservation = db.Reservations.Where(x => x.ApplicationUserId == user.Id)
                    .Where(x => x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending)
                    .ToList();

                return View(new TripDayTripsResponse
                {
                    Trips = trips,
                    From = fromDistrict.FullName,
                    To = toDistrict.FullName,
                    CurrentUserId = user.Id,
                    CurrentDate = d,
                    ExistentReservations = existentReservation,
                    CouldNotFindExactTrip = couldNotFindExactTrip
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
                var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);

                var response = new TripCreateResponse
                {
                    DistrictControlOptions = districtsSelectHtml,
                    Vehicle = user.Vehicle,
                    CountryName = user.Country.Name
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

            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = Common.GetUserByEmail(User.Identity.Name);
            var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);

            try
            {
                #region Fields
                fields += "FromTown: " + Request["FromTown"];
                fields += "ToTown: " + Request["ToTown"] + ", ";
                fields += "Route: " + Request["Route"] + ", ";
                fields += "AvailableSpaces: " + Request["AvailableSpaces"];
                fields += "DateTime: " + Request["DateTime"];
                fields += "Trip.Details: " + Request["Trip.Details"];
                fields += "Trip.Price: " + Request["Trip.Price"];
                fields += "TotalSpaces: " + Request["TotalSpaces"];
                #endregion

                if (ModelState.IsValid)
                {
                    var fromDistrict = new District();
                    var toDistrict = new District();
                    var routeDistrict = new District();

                    var tripDate = Convert.ToDateTime(Request["DateTime"]);
                    fromDistrict = Common.ValidateDistrictString(Request["FromTown"]);

                    if (fromDistrict == null)
                    {
                        //¡Origen no válido!
                        ViewBag.Warning = "10005";

                        var response = new TripCreateResponse
                        {
                            DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                            Trip = trip,
                            Vehicle = user.Vehicle,
                            CountryName = user.Country.Name
                        };

                        return View(response);
                    }

                    toDistrict = Common.ValidateDistrictString(Request["ToTown"]);

                    if (toDistrict == null)
                    {
                        //¡Destino no válido!
                        ViewBag.Warning = "10006";

                        var response = new TripCreateResponse
                        {
                            DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                            Trip = trip,
                            Vehicle = user.Vehicle,
                            CountryName = user.Country.Name
                        };

                        return View(response);
                    }

                    routeDistrict = Common.ValidateDistrictString(Request["Route"]);

                    if (routeDistrict == null)
                    {
                        //¡Destino no válido!
                        ViewBag.Warning = "10006";

                        var response = new TripCreateResponse
                        {
                            DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                            Trip = trip,
                            Vehicle = user.Vehicle,
                            CountryName = user.Country.Name
                        };

                        return View(response);
                    }

                    int routeId = routeDistrict.DistrictId;

                    trip = new Trip
                    {
                        ApplicationUserId = user.Id,
                        AvailableSpaces = Convert.ToInt32(Request["AvailableSpaces"]),
                        CreatedTime = Common.ConvertToUTCTime(DateTime.Now),
                        DateTime = Common.ConvertToUTCTime(tripDate),
                        Details = Request["Trip.Details"],
                        FromTownId = fromDistrict.DistrictId,
                        Price = Convert.ToDecimal(Request["Trip.Price"]),
                        Status = Enums.Status.Activo,
                        TotalSpaces = Convert.ToInt32(Request["TotalSpaces"]),
                        ToTownId = toDistrict.DistrictId,
                        RouteId = routeId
                    };

                    db.Trips.Add(trip);
                    db.SaveChanges();

                    trip.FromTown = fromDistrict;
                    trip.ToTown = toDistrict;
                    trip.Route = routeDistrict;

                    var callbackUrl = Url.Action("Transportation", "Reservations", new { from = trip.FromTown.FullName, to = trip.ToTown.FullName }, protocol: Request.Url.Scheme);

                    var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                    if (send)
                    {
                        FacebookHandler.PublishFacebookPost(trip.FromTown.FullName, trip.ToTown.FullName, trip.Route.Name, trip.DateTime, user.Country.CurrencyChar, trip.Price, trip.AvailableSpaces, callbackUrl);
                    }

                    //EmailHandler.SendEmailTripCreation(WebConfigurationManager.AppSettings["AdminEmails"], user.FullName, tripInfo, trip.AvailableSpaces, callbackUrl);

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

                var response = new TripCreateResponse
                {
                    DistrictControlOptions = districtsSelectHtml,
                    Trip = trip,
                    Vehicle = user.Vehicle,
                    CountryName = user.Country.Name
                };

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View(response);
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
                        FromTownId = Convert.ToInt32(Request["FromTown"]),
                        Price = Convert.ToDecimal(Request["Trip.Price"]),
                        Status = Enums.Status.Activo,
                        TotalSpaces = Convert.ToInt32(Request["TotalSpaces"]),
                        ToTownId = Convert.ToInt32(Request["ToTown"])
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

        public ActionResult LoadDriverTripHistorial(string message)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.Info = message;
                }

                Common.FinilizeExpiredTrips();

                var user = Common.GetUserByEmail(User.Identity.Name);

                List<Trip> trips = new List<Trip>();
                List<Reservation> reservations = new List<Reservation>();

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

                reservations = db.Reservations
                   .Join(db.Trips, r => r.TripId, t => t.TripId, (r, t) => new { r, t })
                   .Where(x => x.r.ApplicationUserId == user.Id)
                   .Where(x => x.t.Status == Status.Finalizado)
                   .Select(m => m.r)
                   .ToList();

                foreach (var trip in trips)
                {
                    trip.Reservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                        .Include(x => x.ApplicationUser)
                        .ToList();

                    trip.Qualifications = db.Qualifications.Where(x => x.TripId == trip.TripId)
                        .Include(x => x.Qualifier)
                        .ToList();

                    trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                    trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();
                    trip.Route = db.Districts.Where(x => x.DistrictId == trip.RouteId).Single();
                }

                foreach (var reservation in reservations)
                {
                    reservation.Trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                        .Include(x => x.FromTown)
                        .Include(x => x.ToTown)
                        .Include(x => x.ApplicationUser)
                        .Single();

                    reservation.Trip.Route = db.Districts.Where(x => x.DistrictId == reservation.Trip.RouteId).Single();
                    reservation.Trip.Qualifications = db.Qualifications.Where(x => x.TripId == reservation.TripId && x.QualifierId != user.Id)
                        .Include(x => x.Qualifier)
                        .ToList();
                    reservation.Qualifications = db.Qualifications.Where(x => x.ReservationId == reservation.ReservationId && x.QualifierId != user.Id)
                        .Include(x => x.Qualifier)
                        .ToList();
                }

                var response = new DriverTripHistorialResponse
                {
                    Trips = trips,
                    Reservations = reservations
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
