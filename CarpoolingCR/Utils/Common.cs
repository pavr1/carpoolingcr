using CarpoolingCR.Models;
using CarpoolingCR.Models.Locations;
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
using System.Web.Mvc;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Utils
{
    public class Common
    {
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

        public static int CalculateOverallUserStars(string userId, bool isDriver)
        {
            using (var db = new ApplicationDbContext())
            {
                List<UserRating> ratings = db.UserRatings.Where(x => x.ToId == userId).ToList();

                var totalStars = 0;

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
                        return 0;
                    }
                    else
                    {
                        foreach (var rating in ratings)
                        {
                            totalStars += rating.Stars;
                        }

                        var average = totalStars / ratings.Count();

                        return average;
                    }
                }
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

            UpdateItemsCount(user.Id);
        }

        public static void FinalizeExpiredReservations(string userId)
        {
            using (var db = new ApplicationDbContext())
            {
                var currentUTCTime = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime());
                var expiredReservations = db.Reservations.Where(x => x.ApplicationUserId == userId && (x.Status == ReservationStatus.Accepted || x.Status == ReservationStatus.Pending))
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

        public static void UpdateItemsCount(string userId)
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
                var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(Identity), new AuthenticationProperties() { IsPersistent = true });
            }
        }
    }
}