using CarpoolingCR.Models;
using CarpoolingCR.Models.Locations;
using CarpoolingCR.Models.Promos;
using CarpoolingCR.Models.Vehicle;
using CarpoolingCR.Objects.Responses;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Utils
{
    public class Common
    {
        public static void ApplyBlockedAmount(Trip trip, ApplicationDbContext db)
        {
            var blockedAmount = db.BlockedAmounts.Where(x => x.TripId == trip.TripId).SingleOrDefault();

            if (blockedAmount == null)
            {
                return;
            }

            try
            {
                if (trip.Status == Status.Cancelado)
                {
                    db.Entry(blockedAmount).State = EntityState.Deleted;
                    db.SaveChanges();

                    var driverBalanceHistorial = new BalanceHistorial
                    {
                        CashAmount = 0m,
                        Date = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                        Detail = "Cancelación de viaje: " + trip.FromTown.FullName + " - " + trip.ToTown.FullName,
                        RidecoinsAmount = blockedAmount.BlockedBalanceAmount * -1,
                        PromoAmount = blockedAmount.PromoAmount * -1,
                        TripId = trip.TripId,
                        UserId = trip.ApplicationUserId,
                    };

                    db.Entry(driverBalanceHistorial).State = EntityState.Added;
                    db.SaveChanges();
                }
                else if (trip.Status == Status.Finalizado)
                {
                    var amount = blockedAmount.BlockedBalanceAmount + blockedAmount.PromoAmount;

                    db.Entry(blockedAmount).State = EntityState.Deleted;
                    db.SaveChanges();

                    var user = db.Users.Where(x => x.Id == trip.ApplicationUserId).Single();

                    user.Ridecoins += amount;

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    var tripPromoHistorial = new BalanceHistorial
                    {
                        RidecoinsAmount = blockedAmount.BlockedBalanceAmount,
                        PromoAmount = blockedAmount.PromoAmount,
                        CashAmount = 0m,
                        Date = trip.DateTime,
                        Detail = "Creación de viaje: " + trip.FromTown.FullName + " - " + trip.ToTown.FullName,
                        TripId = trip.TripId,
                        UserId = trip.ApplicationUserId,
                        UserPromoId = blockedAmount.PromoId
                    };

                    db.Entry(tripPromoHistorial).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ApplyBlockedAmount(Reservation reservation, ApplicationDbContext db)
        {
            var blockedAmount = db.BlockedAmounts.Where(x => x.ReservationId == reservation.ReservationId).SingleOrDefault();

            if (blockedAmount == null)
            {
                return;
            }

            try
            {
                var trip = db.Trips.Where(x => x.TripId == reservation.TripId).Single();
                trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();

                if ((reservation.Status == ReservationStatus.Cancelled) || (reservation.Status == ReservationStatus.Rejected))
                {
                    var rollbackBalance = blockedAmount.BlockedBalanceAmount;

                    db.Entry(blockedAmount).State = EntityState.Deleted;
                    db.SaveChanges();

                    var driverBalanceHistorial = new BalanceHistorial
                    {
                        CashAmount = 0m,
                        Date = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                        Detail = "Cancelación de reservación: " + trip.FromTown.FullName + " - " + trip.ToTown.FullName,
                        RidecoinsAmount = blockedAmount.BlockedBalanceAmount * -1,
                        PromoAmount = blockedAmount.PromoAmount * -1,
                        TripId = reservation.Trip.TripId,
                        UserId = reservation.Trip.ApplicationUserId,
                    };

                    db.Entry(driverBalanceHistorial).State = EntityState.Added;
                    db.SaveChanges();

                    var passengerBalanceHistorial = new BalanceHistorial
                    {
                        CashAmount = 0m,
                        Date = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                        Detail = "Cancelación de reservación: " + trip.FromTown.FullName + " - " + trip.ToTown.FullName,
                        RidecoinsAmount = blockedAmount.BlockedBalanceAmount,
                        PromoAmount = blockedAmount.PromoAmount,
                        TripId = reservation.TripId,
                        UserId = reservation.ApplicationUserId,
                    };

                    db.Entry(passengerBalanceHistorial).State = EntityState.Added;
                    db.SaveChanges();

                    var reservationUser = db.Users.Where(x => x.Id == reservation.ApplicationUserId).Single();
                    reservationUser.Ridecoins += rollbackBalance;

                    db.Entry(reservationUser).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (reservation.Status == ReservationStatus.Finalized)
                {
                    var rollbackBalance = blockedAmount.BlockedBalanceAmount + blockedAmount.PromoAmount;
                    var cashAmount = reservation.totalPayedWithCash;

                    db.Entry(blockedAmount).State = EntityState.Deleted;
                    db.SaveChanges();

                    var tripUser = db.Users.Where(x => x.Id == trip.ApplicationUserId).Single();
                    tripUser.Ridecoins += rollbackBalance;

                    db.Entry(tripUser).State = EntityState.Modified;
                    db.SaveChanges();

                    //add historial for the driver's payment
                    var driverBalanceHistorial = new BalanceHistorial
                    {
                        RidecoinsAmount = blockedAmount.BlockedBalanceAmount,
                        PromoAmount = blockedAmount.PromoAmount,
                        CashAmount = cashAmount,
                        Date = trip.DateTime,
                        Detail = "Pago de viaje: " + trip.FromTown.FullName + " - " + trip.ToTown.FullName,
                        TripId = trip.TripId,
                        UserId = trip.ApplicationUserId,
                        UserPromoId = reservation.PromoId
                    };

                    db.Entry(driverBalanceHistorial).State = EntityState.Added;
                    db.SaveChanges();

                    //add historial for the passenger's payment
                    var passengerBalanceHistorial = new BalanceHistorial
                    {
                        RidecoinsAmount = blockedAmount.BlockedBalanceAmount * -1,
                        PromoAmount = blockedAmount.PromoAmount,
                        CashAmount = cashAmount * -1,
                        Date = trip.DateTime,
                        Detail = "Pago de viaje: " + trip.FromTown.FullName + " - " + trip.ToTown.FullName,
                        TripId = trip.TripId,
                        UserId = reservation.ApplicationUserId,
                        UserPromoId = reservation.PromoId
                    };

                    db.Entry(passengerBalanceHistorial).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetLocationsStrings(int countryId)
        {
            var list = new List<LocationsResponse>();
            list.Add(new LocationsResponse
            {
                DistrictId = -1,
                Display = string.Empty
            });

            using (var db = new ApplicationDbContext())
            {
                var provinces = db.Provinces
                    .Where(x => x.CountryId == countryId)
                    .Select(x => x.ProvinceId)
                    .ToList();

                var counties = db.Counties.Where(x => provinces.Contains(x.ProvinceId))
                        .Select(x => x.CountyId)
                        .ToList();

                var districts = db.Districts
                    .Join(db.Counties, d => d.CountyId, co => co.CountyId, (d, co) => new { d, co })
                    .Where(x => counties.Contains(x.d.CountyId))
                    .Select(m => new LocationsResponse
                    {
                        DistrictId = m.d.DistrictId,
                        Display = m.co.Name + ", " + m.d.Name
                    })
                    .ToList();

                list.AddRange(districts);
            }

            var from = LoadDistrictsSelectControl(list);

            return from;
        }

        public static decimal CalculateOverallUserStars(string userId, bool isDriver)
        {
            using (var db = new ApplicationDbContext())
            {
                List<UserRating> ratings = db.UserRatings.Where(x => x.ToId == userId).ToList();

                var totalStars = 0m;

                //todo: if user has been rated in less than 10 times, not enough data to star, return -1
                //disabled this by now
                if (false)//if (ratings.Count < 10)
                {
                    return -1;
                }
                else
                {
                    if (ratings.Count() == 0)
                    {
                        return 0m;
                    }
                    else
                    {
                        foreach (var rating in ratings)
                        {
                            totalStars += rating.Stars;
                        }

                        var average = Convert.ToDecimal(totalStars / ratings.Count());

                        return average;
                    }
                }
            }
        }


        public static Promo FindAvailablePromo(string promoType, ApplicationUser user)
        {
            using (var db = new ApplicationDbContext())
            {
                var registerPromoType = db.PromoType.Where(x => x.Description == promoType).Single();

                var promo = db.Promo.Where(x => x.PromoTypeId == registerPromoType.PromoTypeId)
                    .Where(x => x.Status == Enums.PromoStatus.Active)
                    .SingleOrDefault();

                if (promo == null)
                {
                    return promo;
                }

                if (Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()) < promo.StartTime)
                {
                    //not in promo's range. Treat it as there is no promo yet
                    promo = null;
                }

                if (promo.EndTime != null)
                {
                    if (Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()) > promo.EndTime)
                    {
                        //not in promo's range. Treat it as there is no promo yet
                        promo = null;
                    }
                }

                if (promo != null)
                {
                    var appliesForPromo = false;

                    //the user can get the promo as many times as he wants
                    if (promo.MaxTimesPerUser == 0)
                    {
                        appliesForPromo = true;
                    }
                    else
                    {
                        var promosAlreadyTaken = db.UserPromos.Where(x => x.PromoId == promo.PromoId && x.UserId == user.Id).ToList();

                        if (promosAlreadyTaken.Count() < promo.MaxTimesPerUser)
                        {
                            appliesForPromo = true;
                        }
                    }

                    var remainingBudget = promo.AmountAvailable - promo.Amount;

                    //if the available amount minus promo amount for this particular user is less than or equal to zero, then the promo is over. Not enough money for this user's bonus
                    //so do not apply the promo and inactivate it
                    if (remainingBudget <= 0)
                    {
                        appliesForPromo = false;

                        promo.Status = Enums.PromoStatus.Inactive;

                        db.Entry(promo).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    //if does not apply promo then set promo to null
                    if (!appliesForPromo)
                    {
                        promo = null;
                    }
                }

                return promo;
            }
        }

        public static string GetLocationName(int districtId)
        {
            using (var db = new ApplicationDbContext())
            {
                var district = db.Districts.Where(x => x.DistrictId == districtId).Single();
                var county = db.Counties.Where(x => x.CountyId == district.CountyId).Single();

                return county.Name + ", " + district.Name;
            }
        }

        public static void GetNearByTripsForReservationTransportation(District fromDistrict, District toDistrict, ref List<Trip> trips, ApplicationUser user, out bool couldNotFindExactTrip)
        {
            couldNotFindExactTrip = false;

            if (trips.Count == 0)
            {
                using (var db = new ApplicationDbContext())
                {
                    var fromCountyDistricts = db.Districts.Where(x => x.CountyId == fromDistrict.CountyId)
                        .Select(x => x.DistrictId)
                        .ToList();

                    var toCountyDistricts = db.Districts.Where(x => x.CountyId == toDistrict.CountyId)
                        .Select(x => x.DistrictId)
                        .ToList();

                    var currentTime = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime());

                    if (user.UserType == Enums.UserType.Administrador)
                    {
                        trips = db.Trips.Where(x => fromCountyDistricts.Contains(x.FromTownId) && toCountyDistricts.Contains(x.ToTownId))
                        .Where(x => x.Status == Status.Activo)
                        .Where(x => x.AvailableSpaces > 0)
                        .Where(x => x.DateTime > currentTime)
                        .ToList();
                    }
                    else
                    {
                        trips = db.Trips.Where(x => fromCountyDistricts.Contains(x.FromTownId) && toCountyDistricts.Contains(x.ToTownId))
                        .Where(x => x.Status == Status.Activo)
                        .Where(x => x.ApplicationUserId != user.Id)
                        .Where(x => x.AvailableSpaces > 0)
                        .Where(x => x.DateTime > currentTime)
                        .ToList();
                    }
                }

                couldNotFindExactTrip = (trips.Count > 0);
            }
        }

        public static void GetNearByTripsForReservationTransportation(District fromDistrict, District toDistrict, ApplicationUser user, ref List<Trip> trips, DateTime startDate, DateTime endDate, out bool couldNotFindExactTrip)
        {
            couldNotFindExactTrip = false;

            if (trips.Count == 0)
            {
                using (var db = new ApplicationDbContext())
                {
                    var fromCountyDistricts = db.Districts.Where(x => x.CountyId == fromDistrict.CountyId)
                        .Select(x => x.DistrictId)
                        .ToList();

                    var toCountyDistricts = db.Districts.Where(x => x.CountyId == toDistrict.CountyId)
                        .Select(x => x.DistrictId)
                        .ToList();

                    var currentTime = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime());

                    trips = db.Trips.Where(x => x.Status == Enums.Status.Activo)
                    .Where(x => x.DateTime >= startDate && x.DateTime <= endDate)
                    .Where(x => fromCountyDistricts.Contains(x.FromTownId) && toCountyDistricts.Contains(x.ToTownId))
                    .Include(x => x.ApplicationUser)
                    .Where(x => x.DateTime > currentTime)
                    .ToList();
                }

                couldNotFindExactTrip = (trips.Count > 0);
            }
        }

        public static District ValidateDistrictString(string data)
        {
            var split = data.Replace("🖣 ", string.Empty).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length < 2)
            {
                return null;
            }

            var countyId = split[0].Trim();
            var districtId = split[1].Trim();
            District result = null;

            using (var db = new ApplicationDbContext())
            {
                var county = db.Counties.Where(x => x.Name == countyId).SingleOrDefault();

                if (county != null)
                {
                    result = db.Districts.Where(x => x.CountyId == county.CountyId && x.Name == districtId).SingleOrDefault();
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackFrame callStack = new StackFrame(1, true);

            return callStack.GetFileName();
        }

        public static int GetCurrentLine()
        {
            StackFrame callStack = new StackFrame(1, true);

            return callStack.GetFileLineNumber();
        }

        public static ApplicationUser GetUserByEmail(string email)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.Where(x => x.Email == email)
                    .Include(x => x.Country)
                    .SingleOrDefault();

                if (user != null)
                {
                    user.Vehicle = db.Vehicles.Where(x => x.ApplicationUserId == user.Id).SingleOrDefault();
                }

                return user;
            }
        }

        public static ApplicationUser GetUserById(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Users.Where(x => x.Id == id).SingleOrDefault();
            }
        }

        public static Enums.UserType GetUserType(string email)
        {
            var userExt = GetUserByEmail(email);

            if (userExt != null)
            {
                return userExt.UserType;
            }

            return Enums.UserType.Pasajero;
        }

        public static int? GetUserCountryId(string email)
        {
            var userExt = GetUserByEmail(email);

            if (userExt != null)
            {
                return userExt.CountryId;
            }

            return -1;
        }

        public static bool IsAuthorized(IPrincipal user)
        {
            return (user.Identity != null && user.Identity.IsAuthenticated);
        }

        public static void LogData(Log log, string logo)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Logs.Add(log);
                db.SaveChanges();

                var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                if (send)
                {
                    EmailHandler.SendEmail(log.Line, log.Location, log.LogType, log.Message, log.Method, log.Timestamp, log.UserEmail, log.Fields, logo);
                }
            }
        }

        public static DateTime ConvertToLocalTime(DateTime dateTime)
        {
            //var timeZone = TimeZoneInfo.FindSystemTimeZoneById(WebConfigurationManager.AppSettings["CR_TimeZone"]);//Common.GetCurrentTimeZoneId());
            //var utcDate = Convert.ToDateTime(dateTime);
            //var localDate = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            var localDate = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);

            return localDate;
        }

        public static DateTime ConvertToUTCTime(DateTime dateTime)
        {
            //var timeZone = TimeZoneInfo.FindSystemTimeZoneById(WebConfigurationManager.AppSettings["CR_TimeZone"]);//Common.GetCurrentTimeZoneId());
            //var utcDate = Convert.ToDateTime(dateTime);
            //var localDate = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            var utcDate = TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local);

            return utcDate;
        }

        public void GetUserInfo(ApplicationUser user)
        {

        }

        public static string GetCurrentTimeZoneId()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var statement = "DECLARE @TimeZone VARCHAR(50) EXEC MASTER.dbo.xp_regread 'HKEY_LOCAL_MACHINE','SYSTEM\\CurrentControlSet\\Control\\TimeZoneInformation','TimeZoneKeyName',@TimeZone OUT SELECT @TimeZone as TimeZone";

            var results = db.Database.SqlQuery<string>(statement, new object[] { });
            var tZ = string.Empty;

            foreach (var item in results)
            {
                return item;
            }

            return string.Empty;
        }

        public static List<Brand> GetAllVehicleBrandsAndModels()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Brands
                    .ToList();
            }
        }

        public static string LoadDistrictsSelectControl(List<LocationsResponse> list)
        {
            var control = string.Empty;// "<select id=\"[control-id]\" name=\"[control-id]\" class=\"form-control waypoints\">";

            control += "<option value=\"-1\"></option>";

            foreach (var item in list)
            {
                control += "<option value=\"" + item.DistrictId + "\">" + item.Display + "</option>";
            }

            //control += "</select>";

            return control;
        }

        public static void FinalizeExpiredTrips(ApplicationUser user)
        {
            using (var db = new ApplicationDbContext())
            {
                var tran = db.Database.BeginTransaction();

                try
                {
                    var currentUTCTime = ConvertToUTCTime(DateTime.Now.ToLocalTime());
                    List<Trip> currentExpiredTrips = new List<Trip>();

                    if (user.UserType == UserType.Administrador)
                    {
                        currentExpiredTrips = db.Trips
                            .Where(x => x.DateTime < currentUTCTime)
                            .Where(x => x.Status != Status.Finalizado)
                            .ToList();
                    }
                    else
                    {
                        currentExpiredTrips = db.Trips
                            .Where(x => x.ApplicationUserId == user.Id)
                            .Where(x => x.DateTime < currentUTCTime)
                            .Where(x => x.Status != Status.Finalizado)
                            .ToList();
                    }

                    foreach (var expiredTrip in currentExpiredTrips)
                    {
                        expiredTrip.Status = Status.Finalizado;

                        db.Entry(expiredTrip).State = EntityState.Modified;
                        db.SaveChanges();

                        expiredTrip.FromTown = db.Districts.Where(x => x.DistrictId == expiredTrip.FromTownId).Single();
                        expiredTrip.ToTown = db.Districts.Where(x => x.DistrictId == expiredTrip.ToTownId).Single();

                        Common.ApplyBlockedAmount(expiredTrip, db);

                        //load all reservations except the ones cancelled/rejected/finalized before, those will remain with that status.
                        var reservations = db.Reservations.Where(x => x.TripId == expiredTrip.TripId)
                            .Where(x => x.Status != ReservationStatus.Cancelled && x.Status != ReservationStatus.Rejected && x.Status != ReservationStatus.Finalized)
                            .ToList();

                        foreach (var expiredReservation in reservations)
                        {
                            //if reservation remains Pending after the trip time, then it will be cancelled.
                            if (expiredReservation.Status == ReservationStatus.Pending)
                            {
                                expiredReservation.Status = ReservationStatus.Cancelled;
                            }
                            else
                            {
                                //if status is accepted, set it to finilized
                                expiredReservation.Status = ReservationStatus.Finalized;
                            }

                            db.Entry(expiredReservation).State = EntityState.Modified;
                            db.SaveChanges();

                            ApplyBlockedAmount(expiredReservation, db);
                        }
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    throw ex;
                }
            }

            UpdateMenuItemsCount(user.Id);
        }

        public static void FinalizeExpiredReservations(string userId)
        {
            using (var db = new ApplicationDbContext())
            {
                var currentUTCTime = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime());
                var expiredReservations = db.Reservations.Where(x => x.ApplicationUserId == userId)
                    .Where(x => (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending))
                    .Where(x => x.Trip.DateTime < currentUTCTime)
                    .ToList();

                //finalizing expired reservations
                foreach (var expiredReservation in expiredReservations)
                {
                    //if reservation remains Pending after the trip time, then it will be cancelled.
                    if (expiredReservation.Status == ReservationStatus.Pending)
                    {
                        expiredReservation.Status = ReservationStatus.Cancelled;
                    }
                    else
                    {
                        //if status is accepted, set it to finilized
                        expiredReservation.Status = ReservationStatus.Finalized;
                    }

                    db.Entry(expiredReservation).State = EntityState.Modified;
                    db.SaveChanges();

                    //start transaction here in case ApplyBlockedAmount throws an error
                    var tran = db.Database.BeginTransaction();

                    try
                    {
                        ApplyBlockedAmount(expiredReservation, db);

                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                    }
                }
            }
        }

        public static int GetRandomPhoneVerificationNumber()
        {
            var from = Convert.ToInt32(WebConfigurationManager.AppSettings["PhoneFromVerificationNumberLength"]);
            var to = Convert.ToInt32(WebConfigurationManager.AppSettings["PhoneToVerificationNumberLength"]);

            Random random = new Random();
            int randomNumber = random.Next(from, to);

            return randomNumber;
        }

        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }

        public static void UpdateMenuItemsCount(string userId)
        {
            using (var db = new ApplicationDbContext())
            {
                var trips = 0;
                var reservations = 0;
                var notifications = 0;

                var user = db.Users.Where(x => x.Id == userId).Single();

                if (user.UserType == Enums.UserType.Administrador)
                {
                    trips = db.Trips.Where(x => (x.Status == Enums.Status.Activo || x.Status == Enums.Status.Lleno || x.Status == Enums.Status.Pendiente)).Count();
                    reservations = db.Reservations.Where(x => (x.Status == Enums.ReservationStatus.Accepted || x.Status == Enums.ReservationStatus.Pending || x.Status == Enums.ReservationStatus.Rejected)).Count();
                    notifications = db.NotificationRequests.Where(x => (x.Status == CarpoolingCR.Utils.Enums.RequestNotificationStatus.Active)).Count();
                }
                else
                {
                    trips = db.Trips.Where(x => x.ApplicationUserId == userId && (x.Status == Enums.Status.Activo || x.Status == Enums.Status.Lleno || x.Status == Enums.Status.Pendiente)).Count();
                    reservations = db.Reservations.Where(x => x.ApplicationUserId == userId && (x.Status == Enums.ReservationStatus.Accepted || x.Status == Enums.ReservationStatus.Pending || x.Status == Enums.ReservationStatus.Rejected)).Count();
                    notifications = db.NotificationRequests.Where(x => x.UserId == userId && (x.Status == CarpoolingCR.Utils.Enums.RequestNotificationStatus.Active)).Count();
                }

                var Identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                Identity.RemoveClaim(Identity.FindFirst("Name"));
                Identity.AddClaim(new Claim("Name", user.Name));

                Identity.RemoveClaim(Identity.FindFirst("Trips"));
                Identity.AddClaim(new Claim("Trips", trips.ToString()));

                Identity.RemoveClaim(Identity.FindFirst("Reservations"));
                Identity.AddClaim(new Claim("Reservations", reservations.ToString()));

                Identity.RemoveClaim(Identity.FindFirst("Notifications"));
                Identity.AddClaim(new Claim("Notifications", notifications.ToString()));

                try
                {
                    Identity.RemoveClaim(Identity.FindFirst("Balance"));
                }
                catch (Exception)
                {
                    //do nothing
                }

                Identity.AddClaim(new Claim("Balance", user.Ridecoins.ToString()));

                var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(Identity), new AuthenticationProperties() { IsPersistent = true });
            }
        }
    }
}