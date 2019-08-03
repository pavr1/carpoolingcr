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
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);

                var response = new ReservationIndexResponse
                {
                    Reservations = db.Reservations.ToList(),
                    Towns = Common.GetLocationsStrings(user.CountryId)//db.Towns.ToList()
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

        public ActionResult Transportation(string message, int from, int to, int? tabIndex)
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

                var passengerReservations = new List<Reservation>();
                var driverTrips = new List<Trip>();
                var user = Common.GetUserByEmail(User.Identity.Name);

                passengerReservations = db.Reservations.Where(x => x.ApplicationUser.Email == User.Identity.Name && (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending))
                    .Include(x => x.ApplicationUser)
                    .ToList();

                foreach (var reservation in passengerReservations)
                {
                    reservation.Trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                        .Include(x => x.ApplicationUser)
                        .SingleOrDefault();
                    reservation.Trip.FromTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.FromTownId).Single();
                    reservation.Trip.ToTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.ToTownId).Single();

                    reservation.Trip.FromTown.County = db.Counties.Where(x => x.CountyId == reservation.Trip.FromTown.CountyId).Single();
                    reservation.Trip.ToTown.County = db.Counties.Where(x => x.CountyId == reservation.Trip.ToTown.CountyId).Single();
                }

                int tabIndexAux = (tabIndex == null) ? 0 : Convert.ToInt32(tabIndex);

                ReservationTransportationResponse response = new ReservationTransportationResponse
                {
                    Trips = new List<Trip>(),
                    PassengerReservations = passengerReservations,
                    DriverTrips = driverTrips,
                    SelectedJourneyId = -1,
                    SelectedRouteIndex = -1,
                    CurrentUserType = user.UserType,
                    Towns = Common.GetLocationsStrings(user.CountryId),//db.Towns.ToList(),
                    From = from,
                    To = to,
                    TabIndex = tabIndexAux
                };

                //if from/to are provided, load the trips for them
                if (from != -1 && to != -1)
                {
                    response.Trips = db.Trips.Where(x => x.FromTownId == from && x.ToTownId == to && x.Status == Status.Activo).ToList();

                    foreach (var trip in response.Trips)
                    {
                        trip.DateTime = Common.ConvertToUTCTime(trip.DateTime);
                    }
                }

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

        [HttpPost]
        public string Transportation(string from, string to)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    RedirectToAction("Login", "Account");
                }

                var fromDistrict = new District();
                var toDistrict = new District();
                var user = Common.GetUserByEmail(User.Identity.Name);

                List<Reservation> passengerReservations = new List<Reservation>();
                List<Trip> driverTrips = new List<Trip>();
                ReservationTransportationResponse response = new ReservationTransportationResponse();

                if (user.UserType == Enums.UserType.Pasajero)
                {
                    passengerReservations = db.Reservations.Where(x => x.ApplicationUser.Email == User.Identity.Name && (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending))
                        .Include(x => x.ApplicationUser)
                        .ToList();

                    foreach (var reservation in passengerReservations)
                    {
                        reservation.Trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                            .Include(x => x.ApplicationUser)
                            .SingleOrDefault();

                        reservation.Trip.FromTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.FromTownId).Single();
                        reservation.Trip.ToTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.ToTownId).Single();
                        reservation.Trip.DateTime = Common.ConvertToLocalTime(reservation.Trip.DateTime);
                    }
                }
                else if (user.UserType == Enums.UserType.Conductor)
                {
                    driverTrips = db.Trips.Where(x => x.ApplicationUser.Email == User.Identity.Name && x.Status == Status.Activo || x.Status == Enums.Status.Pendiente)
                        .Include(x => x.ApplicationUser)
                        .ToList();

                    foreach (var trip in driverTrips)
                    {
                        trip.Reservations = db.Reservations.Where(x => x.TripId == trip.TripId && x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending)
                            .ToList();

                        trip.DateTime = Common.ConvertToLocalTime(trip.DateTime);
                    }

                    passengerReservations = db.Reservations.Where(x => x.ApplicationUser.Email == User.Identity.Name && (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending))
                        .Include(x => x.ApplicationUser)
                        .ToList();
                }
                else if (user.UserType == Enums.UserType.Administrador)
                {
                    var adminTrips = db.Trips.Where(x => x.Status == Status.Activo).ToList();

                    foreach (var trip in adminTrips)
                    {
                        var tripReservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                            .Include(x => x.ApplicationUser)
                            .ToList();

                        passengerReservations.AddRange(tripReservations);
                    }
                }

                List<Trip> trips = new List<Trip>();

                var split = from.Replace("🖣 ", string.Empty).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var countyId = split[0].Trim();
                var districtId = split[1].Trim();

                var county = db.Counties.Where(x => x.Name == countyId).SingleOrDefault();

                if (county != null)
                {
                    fromDistrict = db.Districts.Where(x => x.CountyId == county.CountyId && x.Name == districtId).SingleOrDefault();
                }

                if (fromDistrict == null)
                {
                    response = new ReservationTransportationResponse
                    {
                        Trips = trips,
                        PassengerReservations = passengerReservations,
                        DriverTrips = driverTrips,
                        Towns = Common.GetLocationsStrings(user.CountryId),//db.Towns.ToList(),
                        //¡Origen no válido!
                        Message = "100012"
                    };

                    response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

                    return Serializer.Serialize(response);
                }

                split = to.Replace("🖣 ", string.Empty).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                countyId = split[0].Trim();
                districtId = split[1].Trim();

                county = db.Counties.Where(x => x.Name == countyId).SingleOrDefault();

                if (county != null)
                {
                    toDistrict = db.Districts.Where(x => x.CountyId == county.CountyId && x.Name == districtId).SingleOrDefault();
                }

                if (toDistrict == null)
                {
                    response = new ReservationTransportationResponse
                    {
                        Trips = trips,
                        PassengerReservations = passengerReservations,
                        DriverTrips = driverTrips,
                        Towns = Common.GetLocationsStrings(user.CountryId),//db.Towns.ToList(),
                        //¡Destino no válido!
                        Message = "100013"
                    };

                    response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

                    return Serializer.Serialize(response);
                }

                trips = db.Trips.Where(x => x.FromTownId == fromDistrict.DistrictId && x.ToTownId == toDistrict.DistrictId)
                    .Where(x => x.Status == Status.Activo)
                    .Where(x => x.ApplicationUserId != user.Id)
                    .Where(x => x.AvailableSpaces > 0)
                    .ToList();

                //Setting these attributes county.districts property to null to avoid circular exception
                fromDistrict.County.Districts = null;
                toDistrict.County.Districts = null;

                foreach (var trip in trips)
                {
                    trip.FromTown = fromDistrict;
                    trip.ToTown = toDistrict;
                    trip.DateTime = Common.ConvertToLocalTime(trip.DateTime);
                }

                response = new ReservationTransportationResponse
                {
                    Trips = trips,
                    PassengerReservations = passengerReservations,
                    DriverTrips = driverTrips,
                    Towns = Common.GetLocationsStrings(user.CountryId)//db.Towns.ToList()
                };

                response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

                if (response.Trips.Count == 0)
                {
                    //¡No hay viajes disponibles!
                    response.Message = "100014";
                    response.MessageType = "warning";
                }

                return Serializer.Serialize(response);
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

                return string.Empty;
            }
        }


        [HttpPost]
        public ActionResult ChangeReservationStatus()
        {
            var tran = db.Database.BeginTransaction();

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                if (string.IsNullOrEmpty(Request["reservationId"]))
                {
                    throw new Exception("reservationId not found in form");
                }
                if (string.IsNullOrEmpty(Request["status"]))
                {
                    throw new Exception("status not found in form");
                }

                var reservationId = Convert.ToInt32(Request["reservationId"]);

                ReservationStatus stat = ReservationStatus.Accepted;

                if (!Enum.TryParse(Request["status"], out stat))
                {
                    throw new Exception("Invalid ReservationStatus value: " + Request["Status"]);
                }

                var reservation = db.Reservations.Find(reservationId);

                if (reservation == null)
                {
                    throw new Exception("No se pudo encontrar la reservación. Código: " + reservationId);
                }

                var oldStatus = reservation.Status;

                reservation.Status = stat;

                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();

                var trip = db.Trips.Where(x => x.TripId == reservation.TripId).
                    Include(x => x.ApplicationUser).SingleOrDefault();
                trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();

                trip.FromTown.County = db.Counties.Where(x => x.CountyId == trip.FromTown.CountyId).Single();
                trip.ToTown.County = db.Counties.Where(x => x.CountyId == trip.ToTown.CountyId).Single();

                reservation.ApplicationUser = db.Users.Find(reservation.ApplicationUserId);

                var callbackUrl = Url.Action("Transportation", "Reservations", new { tabIndex = 1 }, protocol: Request.Url.Scheme);

                var message = string.Empty;
                var cancelledFrom = Request["cancelledFrom"];
                var tripInfo = trip.FromTown.County.Name + ", " + trip.FromTown.Name + " -> " + trip.ToTown.County.Name + ", " + trip.ToTown.Name;

                if (stat == ReservationStatus.Accepted)
                {
                    if (trip.AvailableSpaces - reservation.RequestedSpaces < 0)
                    {
                        tran.Rollback();

                        return RedirectToAction("Index", "Trips", new { message = "No hay suficientes espacios!", type = "warning" });
                    }

                    trip.AvailableSpaces -= reservation.RequestedSpaces;
                    db.Entry(trip).State = EntityState.Modified;
                    db.SaveChanges();

                    //¡Reservación Aceptada!
                    message = "100015";

                    EmailHandler.SendReservationStatusChangeByDriver(reservation.ApplicationUser.Email, tripInfo, Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt"), "aceptada", callbackUrl);
                }
                else if (stat == ReservationStatus.Cancelled)
                {
                    //¡Reservación Cancelada!
                    message = "100016";

                    if (oldStatus == ReservationStatus.Accepted)
                    {
                        if (cancelledFrom == "passenger")
                        {
                            EmailHandler.SendReservationStatusCancelledByPassenger(trip.ApplicationUser.Email, tripInfo, Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt"), reservation.RequestedSpaces, callbackUrl);
                        }
                        else if (cancelledFrom == "driver")
                        {
                            EmailHandler.SendReservationStatusCancelledByPassenger(reservation.ApplicationUser.Email, tripInfo, Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt"), reservation.RequestedSpaces, callbackUrl);
                        }

                        trip.AvailableSpaces += reservation.RequestedSpaces;
                        db.Entry(trip).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else if (stat == ReservationStatus.Rejected)
                {
                    //¡Reservación Rechazada!
                    message += "100017";

                    EmailHandler.SendReservationStatusChangeByDriver(reservation.ApplicationUser.Email, tripInfo, Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt"), "rechazada", callbackUrl);
                }

                tran.Commit();

                if (cancelledFrom == "passenger")
                {
                    return RedirectToAction("Transportation", "Reservations", new
                    {
                        message = message,
                        from = -1,
                        to = -1,
                        tabIndex = 1
                    });
                }
                else
                {
                    return RedirectToAction("Index", "Trips", new { message = message, type = "info" });
                }
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

                return RedirectToAction("Index", "Trips", new { message = "Error inesperado, intente de nuevo!", type = "error" });
            }
        }

        public ActionResult ChatTest()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
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
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var trip = db.Trips.Where(x => x.TripId == tripId)
                    .Include(x => x.ApplicationUser)
                    .SingleOrDefault();

                trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();

                var response = new ReservationCreateResponse
                {
                    Trip = trip
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

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create()
        {
            var fields = "Fields => ";

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                #region Fields
                fields += "ReservationDate: " + Request["ReservationDate"];
                fields += "RequestedSpaces: " + Request["RequestedSpaces"] + ", ";
                fields += "TripId: " + Request["TripId"];
                #endregion

                var passenger = Common.GetUserByEmail(User.Identity.Name);
                var date = Convert.ToDateTime(Request["ReservationDate"]);
                var tripId = Convert.ToInt32(Request["TripId"]);

                Reservation reservation = new Reservation
                {
                    ApplicationUserId = passenger.Id,
                    Date = Common.ConvertToUTCTime(date),
                    PassengerName = passenger.Name + " " + passenger.LastName + " " + passenger.SecondLastName,
                    RequestedSpaces = Convert.ToInt32(Request["RequestedSpaces"]),
                    Status = ReservationStatus.Pending,
                    TripId = tripId
                };

                var trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                    .Include(x => x.ApplicationUser)
                    .Single();

                trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();

                trip.FromTown.County = db.Counties.Where(x => x.CountyId == trip.FromTown.CountyId).Single();
                trip.ToTown.County = db.Counties.Where(x => x.CountyId == trip.ToTown.CountyId).Single();

                reservation.ApplicationUserId = passenger.Id;
                reservation.Date = Common.ConvertToUTCTime(DateTime.Now);

                db.Reservations.Add(reservation);
                db.SaveChanges();

                var tripInfo = trip.FromTown.County.Name + ", " + trip.FromTown.Name + " a " + trip.ToTown.County.Name + ", " + trip.ToTown.Name + " el " + Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt");
                var spaces = reservation.RequestedSpaces;

                var callbackUrl = Url.Action("Transportation", "Reservations", new { message = "", tabIndex = 1 }, protocol: Request.Url.Scheme);

                EmailHandler.SendEmailTripReservation(WebConfigurationManager.AppSettings["AdminEmails"], trip.ApplicationUser.Email, reservation.PassengerName, spaces, tripInfo, callbackUrl);

                return RedirectToAction("Transportation", "Reservations", new
                {
                    //¡Reservación Creada, conductor notificado!
                    message = "100018",
                    from = -1,
                    to = -1,
                    tabIndex = 1
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
                    UserEmail = User.Identity.Name,
                    Fields = fields
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
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
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
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

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,TripId,UserEmail,RequestedSpaces,Date,Status")] Reservation reservation)
        {
            try
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

        // GET: Reservations/Delete/5
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
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
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

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
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

        public ActionResult LoadPassengerReservationHistorial()
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);
                var currentUTCTime = Common.ConvertToUTCTime(DateTime.Now);

                var reservations = db.Reservations.Where(x => x.ApplicationUserId == user.Id)
                    .Include(x => x.Trip)
                    .Where(x => x.Trip.DateTime <= currentUTCTime)
                    .ToList();

                foreach (var reservation in reservations)
                {
                    reservation.Qualifications = db.Qualifications.Where(x => x.ToUserId == user.Id)
                        .Include(x => x.FromUser)
                        .Include(x => x.ToUser)
                        .ToList();
                }

                return View(reservations);
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
