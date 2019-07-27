using CarpoolingCR.Models;
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

                var response = new ReservationIndexResponse
                {
                    Reservations = db.Reservations.ToList(),
                    Towns = db.Towns.ToList()
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

                ViewBag.Error = "Error inesperado, intente de nuevo!";

                return View();
            }
        }

        public ActionResult Transportation(string message, string from, string to, int? tabIndex)
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
                    Towns = db.Towns.ToList(),
                    From = from,
                    To = to,
                    TabIndex = tabIndexAux
                };

                //if from/to are provided, load the trips for them
                if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
                {
                    response.Trips = db.Trips.Where(x => x.FromTown == from && x.ToTown == to && x.Status == Status.Activo).ToList();

                    var timeZone = TimeZoneInfo.FindSystemTimeZoneById(WebConfigurationManager.AppSettings["CR_TimeZone"]);

                    foreach (var trip in response.Trips)
                    {
                        trip.DateTime = TimeZoneInfo.ConvertTimeFromUtc(trip.DateTime, timeZone);
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

                ViewBag.Error = "Error inesperado, intente de nuevo!";

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

                var fromRequest = db.Towns.Where(x => x.Name == from).SingleOrDefault();

                if (fromRequest == null)
                {
                    response = new ReservationTransportationResponse
                    {
                        Trips = trips,
                        PassengerReservations = passengerReservations,
                        DriverTrips = driverTrips,
                        Towns = db.Towns.ToList(),
                        Message = "Origen no valido"
                    };

                    response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

                    return Serializer.Serialize(response);
                }

                var toRequest = db.Towns.Where(x => x.Name == to).SingleOrDefault();

                if (toRequest == null)
                {
                    response = new ReservationTransportationResponse
                    {
                        Trips = trips,
                        PassengerReservations = passengerReservations,
                        DriverTrips = driverTrips,
                        Towns = db.Towns.ToList(),
                        Message = "Destino no valido"
                    };

                    response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

                    return Serializer.Serialize(response);
                }

                trips = db.Trips.Where(x => x.FromTown == from && x.ToTown == to && x.Status == Status.Activo).ToList();

                foreach (var trip in trips)
                {
                    trip.DateTime = Common.ConvertToLocalTime(trip.DateTime);
                }

                response = new ReservationTransportationResponse
                {
                    Trips = trips,
                    PassengerReservations = passengerReservations,
                    DriverTrips = driverTrips,
                    Towns = db.Towns.ToList()
                };

                response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

                if (response.Trips.Count == 0)
                {
                    response.Message = "No hay viajes disponibles!";
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

                ViewBag.Error = "Error inesperado, intente de nuevo!";

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

                reservation.ApplicationUser = db.Users.Find(reservation.ApplicationUserId);

                var callbackUrl = Url.Action("Transportation", "Reservations", new { tabIndex = 1 }, protocol: Request.Url.Scheme);

                var message = "Reservación ";
                var cancelledFrom = Request["cancelledFrom"];

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

                    message += "Aceptada!";

                    EmailHandler.SendReservationStatusChangeByDriver(reservation.ApplicationUser.Email, trip.FromTown + " -> " + trip.ToTown, Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt"), "aceptada", callbackUrl);
                }
                else if (stat == ReservationStatus.Cancelled)
                {
                    message += "Cancelada!";

                    if (oldStatus == ReservationStatus.Accepted)
                    {
                        if (cancelledFrom == "passenger")
                        {
                            EmailHandler.SendReservationStatusCancelledByPassenger(trip.ApplicationUser.Email, trip.FromTown + " -> " + trip.ToTown, Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt"), reservation.RequestedSpaces, callbackUrl);
                        }
                        else if (cancelledFrom == "driver")
                        {
                            EmailHandler.SendReservationStatusCancelledByPassenger(reservation.ApplicationUser.Email, trip.FromTown + " -> " + trip.ToTown, Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt"), reservation.RequestedSpaces, callbackUrl);
                        }

                        trip.AvailableSpaces += reservation.RequestedSpaces;
                        db.Entry(trip).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else if (stat == ReservationStatus.Rejected)
                {
                    message += "Rechazada!";

                    EmailHandler.SendReservationStatusChangeByDriver(reservation.ApplicationUser.Email, trip.FromTown + " -> " + trip.ToTown, Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt"), "rechazada", callbackUrl);
                }

                tran.Commit();

                if (cancelledFrom == "passenger")
                {
                    return RedirectToAction("Transportation", "Reservations", new
                    {
                        message = message,
                        from = string.Empty,
                        to = string.Empty,
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

                ViewBag.Error = "Error inesperado, intente de nuevo!";

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

                Reservation reservation = new Reservation
                {
                    ApplicationUserId = passenger.Id,
                    Date = Common.ConvertToUTCTime(date),
                    PassengerName = passenger.Name + " " + passenger.LastName + " " + passenger.SecondLastName,
                    RequestedSpaces = Convert.ToInt32(Request["RequestedSpaces"]),
                    Status = ReservationStatus.Pending,
                    TripId = Convert.ToInt32(Request["TripId"])
                };

                var trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                    .Include(x => x.ApplicationUser)
                    .Single();

                reservation.ApplicationUserId = passenger.Id;
                reservation.Date = Common.ConvertToUTCTime(DateTime.Now);

                db.Reservations.Add(reservation);
                db.SaveChanges();

                var tripInfo = trip.FromTown + " a " + trip.ToTown + " el " + Common.ConvertToLocalTime(trip.DateTime).ToString("dd/MM/yyyy hh:mm:ss tt");
                var spaces = reservation.RequestedSpaces;

                var html = "<html><header></header><body>" + reservation.PassengerName + " ha solicitado " + spaces + " espacios para tu viaje de " + tripInfo + "<br/><br/>Da click <b><a href='" + "callbackUrl" + "'>aquí</a></b> para ver la reserva!</body></html>";

                Common.SendEmail(new IdentityMessage
                {
                    Destination = trip.ApplicationUser.Email,
                    Subject = "Han solicitado espacio en tu vehículo!",
                    Body = html
                });

                return RedirectToAction("Transportation", "Reservations", new
                {
                    message = "Reservacion Creada! Conductor Notificado!",
                    from = string.Empty,
                    to = string.Empty,
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

                ViewBag.Error = "Error inesperado, intente de nuevo!";

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

                ViewBag.Error = "Error inesperado, intente de nuevo!";

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

                ViewBag.Error = "Error inesperado, intente de nuevo!";

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

                ViewBag.Error = "Error inesperado, intente de nuevo!";

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

                ViewBag.Error = "Error inesperado, intente de nuevo!";

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
