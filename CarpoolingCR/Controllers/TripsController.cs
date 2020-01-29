using CarpoolingCR.Models;
using CarpoolingCR.Models.Locations;
using CarpoolingCR.Models.Promos;
using CarpoolingCR.Objects.Responses;
using CarpoolingCR.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
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
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);
                Common.UpdateMenuItemsCount(user.Id);

                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //sets expired trips' status "Finalizado"
                Common.FinalizeExpiredTrips(user);

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
                        trip.CanCancelTrip = true;

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

                        var blockedAmount = db.BlockedAmounts.Where(x => x.ToUserId == user.Id)
                            .Where(x => x.TripId == trip.TripId).SingleOrDefault();

                        if (blockedAmount != null)
                        {
                            trip.PendingPromoAmount = blockedAmount.PromoAmount;
                        }
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
                            trip.CanCancelTrip = true;

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

                            var blockedAmount = db.BlockedAmounts.Where(x => x.ToUserId == user.Id)
                            .Where(x => x.TripId == trip.TripId).SingleOrDefault();

                            if (blockedAmount != null)
                            {
                                trip.PendingPromoAmount = blockedAmount.PromoAmount;
                            }
                        }

                        reachedMaxCount = (currentTrips.Count == maxTripsPerUser);
                    }
                }

                trips.Sort((x, y) => y.CreatedTime.CompareTo(x.CreatedTime));

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
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

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

                return View();
            }
        }

        public ActionResult DayTrips(string date, string from, string to)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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

                Common.UpdateMenuItemsCount(user.Id);

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
                    .ToList();

                var promo = Common.FindAvailablePromo("Viaje Pasajero", user);

                if (promo != null)
                {
                    ViewBag.Promo = promo.Amount;
                    ViewBag.PromoId = promo.PromoId;
                }

                var couldNotFindExactTrip = false;
                Common.GetNearByTripsForReservationTransportation(fromDistrict, toDistrict, user, ref trips, startDate, endDate, out couldNotFindExactTrip);

                for (int i = 0; i < trips.Count; i++)
                {
                    var userId = trips[i].ApplicationUserId;
                    var fromId = trips[i].FromTownId;
                    var toId = trips[i].ToTownId;
                    var routeId = trips[i].RouteId;
                    trips[i].FromTown = db.Districts.Where(x => x.DistrictId == fromId).Single();
                    trips[i].ToTown = db.Districts.Where(x => x.DistrictId == toId).Single();
                    trips[i].Route = db.Districts.Where(x => x.DistrictId == routeId).Single();
                    trips[i].ApplicationUser = db.Users.Where(x => x.Id == userId).Single();
                    // \\ chars giving errors when parsing to Json, so replace them to parse correctly and change back in javascript
                    trips[i].ApplicationUser.Picture = trips[i].ApplicationUser.Picture.Replace("/", "|");
                    trips[i].FromTown.County = null;
                    trips[i].ToTown.County = null;
                    trips[i].Route.County = null;

                    var tripId = trips[i].TripId;
                    trips[i].Reservations = db.Reservations.Where(x => x.TripId == tripId && (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending)).ToList();

                    foreach (var reservation in trips[i].Reservations)
                    {
                        reservation.Trip = null;
                    }

                    //foreach (var rating in trips[i].ApplicationUser.UserRatings)
                    //{
                    //    rating.FromUser = null;
                    //    rating.ToUser = null;
                    //}
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
                    CouldNotFindExactTrip = couldNotFindExactTrip,
                    Currency = user.Country.CurrencyChar
                });
            }
            catch (Exception ex)
            {
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

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

                return View();
            }
        }

        public ActionResult TripDetail(int id)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            try
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
            catch (Exception ex)
            {
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

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

                return View();
            }
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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

                return View(trip);
            }
            catch (Exception ex)
            {
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

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

                return View();
            }
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);
                var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);
                var promo = Common.FindAvailablePromo("Viaje Conductor", user);
                var promoAmount = 0m;

                if (promo != null)
                {
                    promoAmount = promo.Amount;
                }

                var response = new TripCreateResponse
                {
                    DistrictControlOptions = districtsSelectHtml,
                    Vehicle = user.Vehicle,
                    CountryName = user.Country.Name,
                    AvailablePromo = promoAmount
                };

                return View(response);
            }
            catch (Exception ex)
            {
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

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
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");
            var tripBack = new Trip();

            var fields = "Fields => ";

            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = Common.GetUserByEmail(User.Identity.Name);
            var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);

            var tran = db.Database.BeginTransaction();

            try
            {
                #region Fields
                fields += "FromTown: " + Request["FromTown"];
                fields += "ToTown: " + Request["ToTown"] + ", ";
                fields += "Route: " + Request["Route"] + ", ";
                fields += "One Way: " + Request["rbOneWay"] + ", ";
                fields += "AvailableSpaces: " + Request["AvailableSpaces"] + ", ";
                fields += "Trip.Details: " + Request["Trip.Details"] + ", ";
                fields += "DateTime: " + Request["DateTime"] + ", ";
                fields += "Aprox Distance: " + Request["aprox-distance"] + ", ";
                fields += "Aprox Time: " + Request["aprox-time"] + ", ";

                fields += "Route Back: " + Request["Route-To"] + ", ";
                fields += "AvailableSpaces-To: " + Request["AvailableSpaces-To"] + ", ";
                fields += "Trip.Details Back: " + Request["Trip_Details_To"] + ", ";
                fields += "DateTime Back: " + Request["DateTime-To"] + ", ";
                fields += "Trip.Price: " + Request["Trip.Price"] + ", ";
                fields += "ck-pet: " + Request["ck-pet"] + ", ";
                fields += "ck-luggage: " + Request["ck-luggage"] + ", ";
                fields += "ck-smoking: " + Request["ck-smoking"] + ", ";
                #endregion

                Promo promo = null;

                if (ModelState.IsValid)
                {
                    var fromDistrict = new District();
                    var toDistrict = new District();
                    var routeDistrict = new District();

                    var tripDate = DateTime.SpecifyKind(Convert.ToDateTime(Request["DateTime"]), DateTimeKind.Local);

                    var apoxTime = Convert.ToDouble(Request["aprox-time"].Replace(".", ","));
                    var arrivalTime = tripDate.AddHours(apoxTime);

                    fromDistrict = Common.ValidateDistrictString(Request["FromTown"]);

                    if (fromDistrict == null)
                    {
                        //¡Origen no válido!
                        ViewBag.Warning = "10005";

                        var response = new TripCreateResponse
                        {
                            DistrictControlOptions = districtsSelectHtml,
                            Trip = trip,
                            Vehicle = user.Vehicle,
                            CountryName = user.Country.Name
                        };

                        tran.Rollback();

                        return View(response);
                    }

                    toDistrict = Common.ValidateDistrictString(Request["ToTown"]);

                    if (toDistrict == null)
                    {
                        //¡Destino no válido!
                        ViewBag.Warning = "10006";

                        var response = new TripCreateResponse
                        {
                            DistrictControlOptions = districtsSelectHtml,
                            Trip = trip,
                            Vehicle = user.Vehicle,
                            CountryName = user.Country.Name
                        };

                        tran.Rollback();

                        return View(response);
                    }

                    routeDistrict = Common.ValidateDistrictString(Request["Route"]);

                    if (routeDistrict == null)
                    {
                        //¡Destino no válido!
                        ViewBag.Warning = "10006";

                        var response = new TripCreateResponse
                        {
                            DistrictControlOptions = districtsSelectHtml,
                            Trip = trip,
                            Vehicle = user.Vehicle,
                            CountryName = user.Country.Name
                        };

                        tran.Rollback();

                        return View(response);
                    }

                    int routeId = routeDistrict.DistrictId;
                    decimal price = Convert.ToDecimal(Request["Trip.Price"].Replace("₡", string.Empty).Replace(",00", string.Empty));
                    var utcTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local);

                    var existentTrip = db.Trips.Where(x => x.DateTime == utcTime)
                        .Where(x => x.FromTownId == fromDistrict.DistrictId && x.ToTownId == toDistrict.DistrictId)
                        .Where(x => x.ApplicationUserId == user.Id).SingleOrDefault();

                    if (existentTrip == null)
                    {
                        trip = new Trip
                        {
                            ApplicationUserId = user.Id,
                            AvailableSpaces = Convert.ToInt32(Request["AvailableSpaces"]),
                            TotalSpaces = Convert.ToInt32(Request["AvailableSpaces"]) + 1,
                            CreatedTime = utcTime,
                            DateTime = TimeZoneInfo.ConvertTimeToUtc(tripDate, TimeZoneInfo.Local),
                            Details = Request["Trip.Details"],
                            FromTownId = fromDistrict.DistrictId,
                            Price = price,
                            Status = Enums.Status.Activo,
                            ToTownId = toDistrict.DistrictId,
                            RouteId = routeId,
                            AproxDistance = Request["aprox-distance"],
                            ArrivalDateTime = TimeZoneInfo.ConvertTimeToUtc(arrivalTime, TimeZoneInfo.Local),
                            AllowPets = Convert.ToBoolean(Request["ck-pet"] == "on"),
                            AllowLuggage = Convert.ToBoolean(Request["ck-luggage"] == "on"),
                            AllowSmoking = Convert.ToBoolean(Request["ck-smoking"] == "on")
                        };

                        db.Entry(trip).State = EntityState.Added;
                        db.SaveChanges();

                        promo = Common.FindAvailablePromo("Viaje Conductor", user);

                        if (promo != null)
                        {
                            var blockedAmount = new BlockedAmount
                            {
                                BlockedBalanceAmount = 0m,
                                PromoAmount = promo.Amount,
                                //this is for drivers promos, no from user
                                FromUserId = "NO_USER",
                                ToUserId = user.Id,
                                TripId = trip.TripId,
                                PromoId = promo.PromoId,
                                Detail = "Bono por creación de viaje."
                            };

                            db.Entry(blockedAmount).State = EntityState.Added;
                            db.SaveChanges();

                            var userPromo = new UserPromos
                            {
                                Date = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                                PromoId = promo.PromoId,
                                UserId = user.Id,
                                BlockedAmountId = blockedAmount.BlockedAmountId
                            };

                            db.Entry(userPromo).State = EntityState.Added;
                            db.SaveChanges();

                            promo = db.Promo.Where(x => x.PromoId == promo.PromoId).Single();
                            promo.AmountAvailable -= blockedAmount.PromoAmount;
                            db.Entry(promo).State = EntityState.Modified;
                            db.SaveChanges();

                            EmailHandler.SendPromoAppliedEmail(user.Email, promo.Description, promo.Amount, "", logo);
                        }
                    }

                    trip.FromTown = fromDistrict;
                    trip.ToTown = toDistrict;
                    trip.Route = routeDistrict;

                    var callbackUrl = Url.Action("CreateReservation", "Reservations", new { from = trip.FromTown.FullName, to = trip.ToTown.FullName }, protocol: Request.Url.Scheme);

                    var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                    if (send)
                    {
                        FacebookHandler.PublishTripCreation(trip.FromTown.FullName, trip.ToTown.FullName, trip.Route.Name, trip.LocalDateTime, user.Country.CurrencyChar, trip.Price, trip.AvailableSpaces, callbackUrl);
                    }

                    new SignalHandler().SendMessage(Enums.EventTriggered.TripCreated.ToString(), "");

                    callbackUrl = Url.Action("DayTrips", "Trips", new { date = "[date]", from = "[from]", to = "[to]" }, protocol: Request.Url.Scheme);

                    var sendNotificationRequests = new Thread(() => ProcessNotificationRequests(callbackUrl));
                    sendNotificationRequests.Start();

                    if (Request["rb-Round"] == "on")
                    {
                        var routeBack = Common.ValidateDistrictString(Request["Route-To"]);

                        if (routeBack == null)
                        {
                            //¡Ruta no válida!
                            ViewBag.Warning = "100063";

                            var response = new TripCreateResponse
                            {
                                DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "Route-To"),
                                Trip = trip,
                                Vehicle = user.Vehicle,
                                CountryName = user.Country.Name
                            };

                            tran.Rollback();

                            return View(response);
                        }

                        int routeBackId = routeBack.DistrictId;
                        var tripDateTo = DateTime.SpecifyKind(Convert.ToDateTime(Request["DateTime-To"]), DateTimeKind.Local);

                        var arrivalTimeTo = tripDateTo.AddHours(apoxTime);

                        existentTrip = db.Trips.Where(x => x.DateTime == utcTime)
                           .Where(x => x.FromTownId == fromDistrict.DistrictId && x.ToTownId == toDistrict.DistrictId)
                           .Where(x => x.ApplicationUserId == user.Id).SingleOrDefault();

                        if (existentTrip == null)
                        {
                            price = Convert.ToDecimal(Request["Trip.Price.To"].Replace("₡", string.Empty).Replace(",00", string.Empty));

                            tripBack = new Trip
                            {
                                ApplicationUserId = user.Id,
                                AvailableSpaces = Convert.ToInt32(Request["AvailableSpaces-To"]),
                                CreatedTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local),
                                DateTime = TimeZoneInfo.ConvertTimeToUtc(tripDateTo, TimeZoneInfo.Local),
                                Details = Request["Trip_Details_To"],
                                FromTownId = toDistrict.DistrictId,
                                Price = price,
                                Status = Enums.Status.Activo,
                                TotalSpaces = Convert.ToInt32(Request["TotalSpaces"]),
                                ToTownId = fromDistrict.DistrictId,
                                RouteId = routeBackId,
                                AproxDistance = Request["aprox-distance"],
                                ArrivalDateTime = TimeZoneInfo.ConvertTimeToUtc(arrivalTimeTo, TimeZoneInfo.Local),
                                AllowPets = Convert.ToBoolean(Request["ck-pet"] == "on"),
                                AllowLuggage = Convert.ToBoolean(Request["ck-luggage"] == "on"),
                                AllowSmoking = Convert.ToBoolean(Request["ck-smoking"] == "on")
                            };

                            db.Entry(tripBack).State = EntityState.Added;
                            db.SaveChanges();

                            if ((promo.AmountAvailable - promo.Amount) >= 0m)
                            {
                                var blockedAmount = new BlockedAmount
                                {
                                    PromoAmount = promo.Amount,
                                    //this is for drivers promos, no from user
                                    FromUserId = "NO_USER",
                                    ToUserId = user.Id,
                                    TripId = tripBack.TripId,
                                    PromoId = promo.PromoId,
                                    Detail = "Bono por creación de viaje."
                                };

                                db.Entry(blockedAmount).State = EntityState.Added;
                                db.SaveChanges();

                                var userPromo = new UserPromos
                                {
                                    Date = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                                    PromoId = promo.PromoId,
                                    UserId = user.Id,
                                    BlockedAmountId = blockedAmount.BlockedAmountId
                                };

                                db.Entry(userPromo).State = EntityState.Added;
                                db.SaveChanges();

                                promo = db.Promo.Where(x => x.PromoId == promo.PromoId).Single();
                                promo.AmountAvailable -= blockedAmount.PromoAmount;

                                db.Entry(promo).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }

                        tripBack.FromTown = toDistrict;
                        tripBack.ToTown = fromDistrict;
                        tripBack.Route = routeBack;

                        callbackUrl = Url.Action("CreateReservation", "Reservations", new { from = tripBack.FromTown.FullName, to = tripBack.ToTown.FullName }, protocol: Request.Url.Scheme);

                        send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                        if (send)
                        {
                            FacebookHandler.PublishTripCreation(tripBack.FromTown.FullName, tripBack.ToTown.FullName, tripBack.Route.Name, tripBack.LocalDateTime, user.Country.CurrencyChar, tripBack.Price, tripBack.AvailableSpaces, callbackUrl);
                        }

                        new SignalHandler().SendMessage(Enums.EventTriggered.TripCreated.ToString(), "");

                        callbackUrl = Url.Action("DayTrips", "Trips", new { date = "[date]", from = "[from]", to = "[to]" }, protocol: Request.Url.Scheme);

                        sendNotificationRequests = new Thread(() => ProcessNotificationRequests(callbackUrl));
                        sendNotificationRequests.Start();
                    }

                    Common.UpdateMenuItemsCount(user.Id);

                    tran.Commit();

                    //¡Viaje Creado!
                    return RedirectToAction("Index", new { message = "10007", type = "info" });
                }

                //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", trip.JourneyId);
                return View();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = User.Identity.Name,
                    Fields = fields
                }, logo);

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

        private void ProcessNotificationRequests(string callback)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            using (var db = new ApplicationDbContext())
            {
                var notifications = db.NotificationRequests.Where(x => x.Status == RequestNotificationStatus.Active)
                .ToList();

                foreach (var not in notifications)
                {
                    var notification = db.NotificationRequests.Where(x => x.NotificationRequestId == not.NotificationRequestId).Single();
                    notification.User = db.Users.Where(x => x.Id == notification.UserId).Single();
                    notification.FromTown = db.Districts.Where(x => x.DistrictId == notification.FromTownId).Single();
                    notification.ToTown = db.Districts.Where(x => x.DistrictId == notification.ToTownId).Single();

                    //if notification time has expired
                    if (notification.RequestedToDateTime < Common.ConvertToUTCTime(DateTime.Now))
                    {
                        notification.Status = RequestNotificationStatus.Expired;

                        try
                        {
                            db.Entry(notification).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        catch (Exception)
                        {
                            //TODO: There is an exception at this point but it still changes it's status so by now just let it crash, later fix
                            //Error: Store update, insert, or delete statement affected an unexpected number of rows (0). Entities may have been modified or deleted since entities were loaded.
                        }
                    }
                    else
                    {
                        //date validation is being done UTC time, not local
                        var fromToTrips = db.Trips.Where(x => x.FromTownId == notification.FromTownId)
                            .Where(x => x.ToTownId == notification.ToTownId)
                            .Where(x => x.DateTime >= notification.RequestedFromDateTime && x.DateTime <= notification.RequestedToDateTime)
                            .Where(x => x.Status == Status.Activo)
                            .ToList();

                        //trip found, send notification email
                        if (fromToTrips.Count > 0)
                        {
                            var userEmail = notification.User.Email;
                            var tripInfo = notification.FromTown.FullName + " a " + notification.ToTown.FullName + " entre " + notification.RequestedFromDateTime.ToString("hh:mm tt") + " y " + notification.RequestedToDateTime.ToString("hh:mm tt");
                            callback = callback.Replace("%5Bdate%5D", notification.RequestedFromDateTime.ToString("yyyy-mm-dd"));
                            callback = callback.Replace("%5Bfrom%5D", notification.FromTown.FullName);
                            callback = callback.Replace("%5Bto%5D", notification.ToTown.FullName);

                            EmailHandler.SendTripNotification(userEmail, DateTime.Now.ToString("dddd d, MMMM yyyy"), tripInfo, callback, logo);
                        }
                    }
                }
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
            //        Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
            //        Method = Common.GetCurrentMethod(),
            //        Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
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
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

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

                return View();
            }
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

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

                return View();
            }
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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

                trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();

                var passengersToNoticeEmail = string.Empty;

                foreach (var reservation in trip.Reservations)
                {
                    reservation.Status = ReservationStatus.Cancelled;
                    db.Entry(reservation).State = EntityState.Modified;
                    db.SaveChanges();

                    Common.ApplyBlockedAmount(reservation, db);

                    passengersToNoticeEmail += reservation.ApplicationUser.Email + ",";
                }

                if (passengersToNoticeEmail.Length > 0)
                {
                    passengersToNoticeEmail = passengersToNoticeEmail.Substring(0, passengersToNoticeEmail.Length - 1);
                }

                trip.Status = Status.Cancelado;

                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();

                Common.ApplyBlockedAmount(trip, db);

                if (passengersToNoticeEmail.Length > 0)
                {
                    var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                    if (send)
                    {
                        //todo: send emails separately
                        EmailHandler.SendTripsCancelledByDriver(passengersToNoticeEmail, trip.FromTown + " -> " + trip.ToTown, trip.LocalDateTime.ToString(WebConfigurationManager.AppSettings["DateTimeFormat"]), string.Empty, logo);
                    }
                }

                tran.Commit();

                var user = Common.GetUserByEmail(User.Identity.Name);
                Common.UpdateMenuItemsCount(user.Id);

                return RedirectToAction("Index", new { message = "Viaje Eliminado!", type = "info" });
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

                return View();
            }
        }

        public ActionResult LoadDriverTripHistorial(string message)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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

                var user = Common.GetUserByEmail(User.Identity.Name);

                Common.FinalizeExpiredTrips(user);

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

                trips.Sort((x, y) => DateTime.Compare(y.LocalDateTime, x.LocalDateTime));

                foreach (var trip in trips)
                {
                    trip.Reservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                        .Include(x => x.ApplicationUser)
                        .ToList();

                    //trip.Qualifications = db.Qualifications.Where(x => x.TripId == trip.TripId)
                    //    .Include(x => x.Qualifier)
                    //    .ToList();

                    trip.ApplicationUser = db.Users.Where(x => x.Id == trip.ApplicationUserId).Single();
                    trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                    trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();
                    trip.Route = db.Districts.Where(x => x.DistrictId == trip.RouteId).Single();
                    trip.UserRatings = db.UserRatings.Where(x => x.TripId == trip.TripId).ToList();
                }

                var response = new HistorialResponse
                {
                    Trips = trips
                };

                return View(response);
            }
            catch (Exception ex)
            {
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

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
