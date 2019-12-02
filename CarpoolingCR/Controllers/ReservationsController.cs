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
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);
                Common.UpdateMenuItemsCount(user.Id);

                var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);

                var response = new ReservationIndexResponse
                {
                    Reservations = db.Reservations.ToList(),
                    DistrictControlOptions = districtsSelectHtml
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

        public ActionResult ReservationIndex(string message)
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

                var passengerReservations = new List<Reservation>();
                var driverTrips = new List<Trip>();
                var user = Common.GetUserByEmail(User.Identity.Name);

                Common.FinalizeExpiredReservations(user.Id);

                if (user.UserType != UserType.Pasajero)
                {
                    Common.FinalizeExpiredTrips(user);
                }

                Common.UpdateMenuItemsCount(user.Id);

                if (user.UserType == UserType.Administrador)
                {
                    passengerReservations = db.Reservations.Where(x => (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending || x.Status == ReservationStatus.Rejected))
                        .Include(x => x.ApplicationUser)
                        .ToList();
                }
                else
                {
                    passengerReservations = db.Reservations.Where(x => x.ApplicationUser.Email == User.Identity.Name && (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending || x.Status == ReservationStatus.Rejected))
                        .Include(x => x.ApplicationUser)
                        .ToList();
                }

                foreach (var reservation in passengerReservations)
                {
                    reservation.Trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                        .Include(x => x.ApplicationUser)
                        .SingleOrDefault();

                    reservation.Trip.FromTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.FromTownId).Single();
                    reservation.Trip.ToTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.ToTownId).Single();
                    reservation.Trip.Route = db.Districts.Where(x => x.DistrictId == reservation.Trip.RouteId).Single();

                    reservation.Trip.UserRatings = db.UserRatings.Where(x => x.TripId == reservation.TripId).ToList();
                }

                List<Trip> trips = new List<Trip>();
                //var fromStr = string.Empty;
                //var toStr = string.Empty;

                ////if from/to are provided, load the trips for them
                //if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
                //{
                //    var fromDistrict = Common.ValidateDistrictString(from);
                //    var toDistrict = Common.ValidateDistrictString(to);

                //    if (fromDistrict != null && toDistrict != null)
                //    {
                //        fromStr = fromDistrict.FullName;
                //        toStr = toDistrict.FullName;

                //        trips = db.Trips.Where(x => x.FromTownId == fromDistrict.DistrictId && x.ToTownId == toDistrict.DistrictId && x.Status == Status.Activo).ToList();

                //        foreach (var trip in trips)
                //        {
                //            trip.DateTime = Common.ConvertToUTCTime(trip.DateTime);
                //        }
                //    }
                //}

                var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);

                ReservationTransportationResponse response = new ReservationTransportationResponse
                {
                    Trips = trips,
                    PassengerReservations = passengerReservations,
                    DriverTrips = driverTrips,
                    SelectedJourneyId = -1,
                    SelectedRouteIndex = -1,
                    CurrentUserType = user.UserType,
                    DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                    //From = fromStr,
                    //To = toStr,
                    //TabIndex = tabIndexAux
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

        public ActionResult CreateReservation(string message)
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

                var passengerReservations = new List<Reservation>();
                var driverTrips = new List<Trip>();
                var user = Common.GetUserByEmail(User.Identity.Name);

                if (user.UserType != UserType.Pasajero)
                {
                    Common.FinalizeExpiredTrips(user);
                }

                Common.FinalizeExpiredReservations(user.Id);

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
                    reservation.Trip.Route = db.Districts.Where(x => x.DistrictId == reservation.Trip.RouteId).Single();

                    reservation.Trip.UserRatings = db.UserRatings.Where(x => x.TripId == reservation.TripId).ToList();
                }


                var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);

                List<Trip> trips = new List<Trip>();

                var promo = Common.FindAvailablePromo("Viaje Pasajero", user);
                var availablePromo = (promo != null) ? promo.Amount : 0m;

                ReservationTransportationResponse response = new ReservationTransportationResponse
                {
                    Trips = trips,
                    PassengerReservations = passengerReservations,
                    DriverTrips = driverTrips,
                    SelectedJourneyId = -1,
                    SelectedRouteIndex = -1,
                    CurrentUserType = user.UserType,
                    DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                    AvailablePromo = availablePromo
                    //From = fromStr,
                    //To = toStr,
                    //TabIndex = tabIndexAux
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

        [HttpPost]
        public string CreateReservation(string from, string to)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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
                    passengerReservations = db.Reservations.Where(x => x.ApplicationUserId == user.Id && (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending))
                        .Include(x => x.ApplicationUser)
                        .ToList();

                    foreach (var reservation in passengerReservations)
                    {
                        reservation.Trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                            .Include(x => x.ApplicationUser)
                            .SingleOrDefault();

                        reservation.Trip.FromTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.FromTownId).Single();
                        reservation.Trip.ToTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.ToTownId).Single();
                        reservation.Trip.Route = db.Districts.Where(x => x.DistrictId == reservation.Trip.RouteId).Single();
                    }
                }
                else if (user.UserType == Enums.UserType.Conductor)
                {
                    driverTrips = db.Trips.Where(x => x.ApplicationUser.Email == User.Identity.Name && x.Status == Status.Activo || x.Status == Enums.Status.Pendiente)
                        .Include(x => x.ApplicationUser)
                        .ToList();

                    foreach (var trip in driverTrips)
                    {
                        trip.Reservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                            .Where(x => x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending)
                            .ToList();

                        foreach (var reservation in trip.Reservations)
                        {
                            reservation.Trip = null;
                        }

                        trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                        trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();
                        trip.Route = db.Districts.Where(x => x.DistrictId == trip.RouteId).Single();
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
                var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);

                fromDistrict = Common.ValidateDistrictString(from);

                if (fromDistrict == null)
                {
                    response = new ReservationTransportationResponse
                    {
                        Trips = trips,
                        PassengerReservations = passengerReservations,
                        DriverTrips = driverTrips,
                        DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                        //¡Origen no válido!
                        Message = "10005"
                    };

                    response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

                    return Serializer.Serialize(response);
                }

                toDistrict = Common.ValidateDistrictString(to);

                if (toDistrict == null)
                {
                    response = new ReservationTransportationResponse
                    {
                        Trips = trips,
                        PassengerReservations = passengerReservations,
                        DriverTrips = driverTrips,
                        DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                        //¡Destino no válido!
                        Message = "100013"
                    };

                    response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

                    return Serializer.Serialize(response);
                }

                var currentTime = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime());

                if (user.UserType == Enums.UserType.Administrador)
                {
                    trips = db.Trips.Where(x => x.FromTownId == fromDistrict.DistrictId && x.ToTownId == toDistrict.DistrictId)
                   .Where(x => x.Status == Status.Activo)
                   .Where(x => x.DateTime > currentTime)
                   .Where(x => x.AvailableSpaces > 0)
                   .ToList();
                }
                else
                {
                    trips = db.Trips.Where(x => x.FromTownId == fromDistrict.DistrictId && x.ToTownId == toDistrict.DistrictId)
                    .Where(x => x.Status == Status.Activo)
                    .Where(x => x.ApplicationUserId != user.Id)
                    .Where(x => x.DateTime > currentTime)
                    .Where(x => x.AvailableSpaces > 0)
                    .ToList();
                }

                var couldNotFindExactTrip = false;

                //if no trips found, check filtering by county instead of districts
                Common.GetNearByTripsForReservationTransportation(fromDistrict, toDistrict, ref trips, user, out couldNotFindExactTrip);

                //Setting these attributes county.districts property to null to avoid circular exception
                fromDistrict.County.Districts = null;
                toDistrict.County.Districts = null;

                trips.Sort((x, y) => y.CreatedTime.CompareTo(x.CreatedTime));

                foreach (var trip in trips)
                {
                    trip.FromTown = fromDistrict;
                    trip.ToTown = toDistrict;
                }

                response = new ReservationTransportationResponse
                {
                    Trips = trips,
                    PassengerReservations = passengerReservations,
                    DriverTrips = driverTrips,
                    DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                    CouldNotFindExactTrip = couldNotFindExactTrip
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

                return string.Empty;
            }
        }

        public ActionResult Transportation(string message, string from, string to, int? tabIndex)
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

                var passengerReservations = new List<Reservation>();
                var driverTrips = new List<Trip>();
                var user = Common.GetUserByEmail(User.Identity.Name);

                if (user.UserType != UserType.Pasajero)
                {
                    Common.FinalizeExpiredTrips(user);
                }

                Common.FinalizeExpiredReservations(user.Id);

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
                    reservation.Trip.Route = db.Districts.Where(x => x.DistrictId == reservation.Trip.RouteId).Single();

                    reservation.Trip.UserRatings = db.UserRatings.Where(x => x.TripId == reservation.TripId).ToList();
                }

                int tabIndexAux = (tabIndex == null) ? 0 : Convert.ToInt32(tabIndex);

                List<Trip> trips = new List<Trip>();
                var fromStr = string.Empty;
                var toStr = string.Empty;

                //if from/to are provided, load the trips for them
                if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
                {
                    var fromDistrict = Common.ValidateDistrictString(from);
                    var toDistrict = Common.ValidateDistrictString(to);

                    if (fromDistrict != null && toDistrict != null)
                    {
                        fromStr = fromDistrict.FullName;
                        toStr = toDistrict.FullName;

                        trips = db.Trips.Where(x => x.FromTownId == fromDistrict.DistrictId && x.ToTownId == toDistrict.DistrictId && x.Status == Status.Activo).ToList();
                    }
                }

                var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);

                trips.Sort((x, y) => y.CreatedTime.CompareTo(x.CreatedTime));

                ReservationTransportationResponse response = new ReservationTransportationResponse
                {
                    Trips = trips,
                    PassengerReservations = passengerReservations,
                    DriverTrips = driverTrips,
                    SelectedJourneyId = -1,
                    SelectedRouteIndex = -1,
                    CurrentUserType = user.UserType,
                    DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                    From = fromStr,
                    To = toStr,
                    TabIndex = tabIndexAux
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

        [HttpPost]
        public string Transportation(string from, string to)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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
                    passengerReservations = db.Reservations.Where(x => x.ApplicationUserId == user.Id && (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending))
                        .Include(x => x.ApplicationUser)
                        .ToList();

                    foreach (var reservation in passengerReservations)
                    {
                        reservation.Trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                            .Include(x => x.ApplicationUser)
                            .SingleOrDefault();

                        reservation.Trip.FromTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.FromTownId).Single();
                        reservation.Trip.ToTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.ToTownId).Single();
                        reservation.Trip.Route = db.Districts.Where(x => x.DistrictId == reservation.Trip.RouteId).Single();
                    }
                }
                else if (user.UserType == Enums.UserType.Conductor)
                {
                    driverTrips = db.Trips.Where(x => x.ApplicationUser.Email == User.Identity.Name && x.Status == Status.Activo || x.Status == Enums.Status.Pendiente)
                        .Include(x => x.ApplicationUser)
                        .ToList();

                    driverTrips.Sort((x, y) => y.CreatedTime.CompareTo(x.CreatedTime));

                    foreach (var trip in driverTrips)
                    {
                        trip.Reservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                            .Where(x => x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending)
                            .ToList();

                        foreach (var reservation in trip.Reservations)
                        {
                            reservation.Trip = null;
                        }

                        trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                        trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();
                        trip.Route = db.Districts.Where(x => x.DistrictId == trip.RouteId).Single();
                    }

                    passengerReservations = db.Reservations.Where(x => x.ApplicationUser.Email == User.Identity.Name && (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending))
                        .Include(x => x.ApplicationUser)
                        .ToList();
                }
                else if (user.UserType == Enums.UserType.Administrador)
                {
                    var adminTrips = db.Trips.Where(x => x.Status == Status.Activo).ToList();

                    adminTrips.Sort((x, y) => y.CreatedTime.CompareTo(x.CreatedTime));

                    foreach (var trip in adminTrips)
                    {
                        var tripReservations = db.Reservations.Where(x => x.TripId == trip.TripId)
                            .Include(x => x.ApplicationUser)
                            .ToList();

                        passengerReservations.AddRange(tripReservations);
                    }
                }

                List<Trip> trips = new List<Trip>();
                var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);

                fromDistrict = Common.ValidateDistrictString(from);

                if (fromDistrict == null)
                {
                    response = new ReservationTransportationResponse
                    {
                        Trips = trips,
                        PassengerReservations = passengerReservations,
                        DriverTrips = driverTrips,
                        DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                        //¡Origen no válido!
                        Message = "10005"
                    };

                    response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

                    return Serializer.Serialize(response);
                }

                toDistrict = Common.ValidateDistrictString(to);

                if (toDistrict == null)
                {
                    response = new ReservationTransportationResponse
                    {
                        Trips = trips,
                        PassengerReservations = passengerReservations,
                        DriverTrips = driverTrips,
                        DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                        //¡Destino no válido!
                        Message = "100013"
                    };

                    response.Html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_RequestJourney", response);

                    return Serializer.Serialize(response);
                }

                var currentTime = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime());

                if (user.UserType == Enums.UserType.Administrador)
                {
                    trips = db.Trips.Where(x => x.FromTownId == fromDistrict.DistrictId && x.ToTownId == toDistrict.DistrictId)
                   .Where(x => x.Status == Status.Activo)
                   .Where(x => x.DateTime > currentTime)
                   .Where(x => x.AvailableSpaces > 0)
                   .ToList();
                }
                else
                {
                    trips = db.Trips.Where(x => x.FromTownId == fromDistrict.DistrictId && x.ToTownId == toDistrict.DistrictId)
                    .Where(x => x.Status == Status.Activo)
                    .Where(x => x.ApplicationUserId != user.Id)
                    .Where(x => x.DateTime > currentTime)
                    .Where(x => x.AvailableSpaces > 0)
                    .ToList();
                }

                var couldNotFindExactTrip = false;

                //if no trips found, check filtering by county instead of districts
                Common.GetNearByTripsForReservationTransportation(fromDistrict, toDistrict, ref trips, user, out couldNotFindExactTrip);

                //Setting these attributes county.districts property to null to avoid circular exception
                fromDistrict.County.Districts = null;
                toDistrict.County.Districts = null;

                foreach (var trip in trips)
                {
                    trip.FromTown = fromDistrict;
                    trip.ToTown = toDistrict;
                }

                trips.Sort((x, y) => y.CreatedTime.CompareTo(x.CreatedTime));

                response = new ReservationTransportationResponse
                {
                    Trips = trips,
                    PassengerReservations = passengerReservations,
                    DriverTrips = driverTrips,
                    DistrictControlOptions = districtsSelectHtml.Replace("[control-id]", "FromTown"),
                    CouldNotFindExactTrip = couldNotFindExactTrip
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

                return string.Empty;
            }
        }


        [HttpPost]
        public ActionResult ChangeReservationStatus()
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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

                //get trip's available spaces back
                if (reservation.Status == ReservationStatus.Cancelled || reservation.Status == ReservationStatus.Rejected)
                {
                    var rollingBacktrip = db.Trips.Where(x => x.TripId == reservation.TripId).Single();
                    rollingBacktrip.AvailableSpaces += reservation.RequestedSpaces;

                    db.Entry(rollingBacktrip).State = EntityState.Modified;
                    db.SaveChanges();
                }

                Common.ApplyBlockedAmount(reservation, db);

                var trip = db.Trips.Where(x => x.TripId == reservation.TripId).
                    Include(x => x.ApplicationUser).SingleOrDefault();

                trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();

                reservation.ApplicationUser = db.Users.Find(reservation.ApplicationUserId);

                var callbackUrl = Url.Action("ReservationIndex", "Reservations", new { }, protocol: Request.Url.Scheme);

                var message = string.Empty;
                var cancelledFrom = Request["cancelledFrom"];
                var tripInfo = trip.FromTown.FullName + " -> " + trip.ToTown.FullName;

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

                    var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                    if (send)
                    {
                        EmailHandler.SendReservationStatusChangeByDriver(reservation.ApplicationUser.Email, tripInfo, trip.LocalDateTime.ToString(WebConfigurationManager.AppSettings["DateTimeFormat"]), "aceptada", callbackUrl, logo);
                    }
                }
                else if (stat == ReservationStatus.Cancelled)
                {
                    //¡Reservación Cancelada!
                    message = "100016";

                    if (oldStatus == ReservationStatus.Accepted)
                    {
                        var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                        if (send)
                        {
                            if (cancelledFrom == "passenger")
                            {
                                EmailHandler.SendReservationStatusCancelledByPassenger(trip.ApplicationUser.Email, tripInfo, trip.LocalDateTime.ToString(WebConfigurationManager.AppSettings["DateTimeFormat"]), reservation.RequestedSpaces, callbackUrl, logo);
                            }
                            else if (cancelledFrom == "driver")
                            {
                                EmailHandler.SendReservationStatusCancelledByPassenger(reservation.ApplicationUser.Email, tripInfo, trip.LocalDateTime.ToString(WebConfigurationManager.AppSettings["DateTimeFormat"]), reservation.RequestedSpaces, callbackUrl, logo);
                            }
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

                    var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                    if (send)
                    {
                        EmailHandler.SendReservationStatusChangeByDriver(reservation.ApplicationUser.Email, tripInfo, trip.LocalDateTime.ToString(WebConfigurationManager.AppSettings["DateTimeFormat"]), "rechazada", callbackUrl, logo);
                    }
                }

                tran.Commit();

                var user = Common.GetUserByEmail(User.Identity.Name);
                Common.UpdateMenuItemsCount(user.Id);

                if (cancelledFrom == "passenger")
                {
                    return RedirectToAction("ReservationIndex", "Reservations", new
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
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create()
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            var fields = "Fields => ";

            var tran = db.Database.BeginTransaction();

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                #region Fields
                fields += "ReservationDate: " + Request["ReservationDate"] + ", ";
                fields += "RequestedSpaces: " + Request["RequestedSpaces"] + ", ";
                fields += "SpacesSelected: " + Request["SpacesSelected"] + ", ";
                fields += "actualBalance: " + Request["actualBalance"] + ", ";
                fields += "SelectedSeatsTotalPrice: " + Request["SelectedSeatsTotalPrice"] + ", ";
                fields += "balancePayment: " + Request["balancePayment"] + ", ";
                fields += "cashPayment: " + Request["cashPayment"] + ", ";
                fields += "TripId: " + Request["TripId"];
                #endregion

                var user = Common.GetUserByEmail(User.Identity.Name);
                var date = Convert.ToDateTime(Request["ReservationDate"]);
                var tripId = Convert.ToInt32(Request["TripId"]);

                Reservation reservation = new Reservation
                {
                    ApplicationUserId = user.Id,
                    Date = Common.ConvertToUTCTime(date),
                    PassengerName = user.Name + " " + user.LastName + " " + user.SecondLastName,
                    RequestedSpaces = Convert.ToInt32(Request["RequestedSpaces"]),
                    SpacesSelected = Request["SpacesSelected"],
                    SelectedSeatsTotalPrice = Convert.ToDecimal(Request["SelectedSeatsTotalPrice"].Replace(".", ",")),
                    totalPayedWithBalance = Convert.ToDecimal(Request["balancePayment"].Replace(".", ",")),
                    totalPayedWithCash = Convert.ToDecimal(Request["cashPayment"].Replace(".", ",")),
                    totalPayedWithPromo = Convert.ToDecimal(Request["bonoPayment"].Replace(".", ",")),
                    Status = ReservationStatus.Pending,
                    TripId = tripId
                };

                var trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                    .Include(x => x.ApplicationUser)
                    .Single();

                trip.AvailableSpaces -= reservation.RequestedSpaces;

                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();

                trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();

                reservation.ApplicationUserId = user.Id;
                reservation.Date = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime());

                db.Reservations.Add(reservation);
                db.SaveChanges();

                var blockedBalance = new BlockedAmount
                {
                    BlockedBalanceAmount = Convert.ToDecimal(Request["balancePayment"].Replace(".", ",")),
                    ReservationId = reservation.ReservationId,
                    FromUserId = reservation.ApplicationUserId,
                    ToUserId = trip.ApplicationUserId
                };

                db.BlockedAmounts.Add(blockedBalance);

                user = db.Users.Where(x => x.Id == user.Id).Single();
                user.PromoBalance = Convert.ToDecimal(Request["actualBalance"].Replace(".", ","));

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                tran.Commit();

                var tripInfo = trip.FromTown.FullName + " a " + trip.ToTown.FullName + " el " + trip.LocalDateTime.ToString(WebConfigurationManager.AppSettings["DateTimeFormat"]);
                var spaces = reservation.RequestedSpaces;

                var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                if (send)
                {
                    var callbackUrl = Url.Action("ReservationIndex", "Reservations", new { message = "" }, protocol: Request.Url.Scheme);

                    EmailHandler.SendEmailTripReservation(WebConfigurationManager.AppSettings["AdminEmails"], trip.ApplicationUser.Email, reservation.PassengerName, spaces, tripInfo, callbackUrl, logo);
                }

                Common.UpdateMenuItemsCount(user.Id);

                return RedirectToAction("ReservationIndex", "Reservations", new
                {
                    //¡Reservación Creada, conductor notificado!
                    message = "100018"
                });
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

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
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
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
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

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,TripId,UserEmail,RequestedSpaces,Date,Status")] Reservation reservation)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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

        // GET: Reservations/Delete/5
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
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
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

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                Reservation reservation = db.Reservations.Find(id);
                db.Reservations.Remove(reservation);
                db.SaveChanges();

                return RedirectToAction("ReservationIndex");
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

        public ActionResult LoadPassengerReservationHistorial(string message)
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
                var currentUTCTime = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime());

                var reservations = db.Reservations.Where(x => x.ApplicationUserId == user.Id)
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Trip)
                    .Where(x => x.Trip.DateTime <= currentUTCTime)
                    .ToList();

                foreach (var reservation in reservations)
                {
                    reservation.Trip = db.Trips.Where(x => x.TripId == reservation.TripId)
                        .Include(x => x.ApplicationUser)
                        .Single();

                    reservation.Trip.FromTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.FromTownId).Single();
                    reservation.Trip.ToTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.ToTownId).Single();
                    reservation.Trip.Route = db.Districts.Where(x => x.DistrictId == reservation.Trip.RouteId).Single();
                    reservation.Trip.UserRatings = db.UserRatings.Where(x => x.TripId == reservation.TripId).ToList();
                }

                var response = new HistorialResponse
                {
                    Reservations = reservations
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
