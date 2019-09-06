﻿using CarpoolingCR.Models;
using CarpoolingCR.Models.Locations;
using CarpoolingCR.Models.Vehicle;
using CarpoolingCR.Objects.Responses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Web.Configuration;
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
                List<Qualification> qualifications = null;

                if (isDriver)
                {
                    var trips = db.Trips.Where(x => x.ApplicationUserId == userId)
                        .Select(x => x.TripId)
                        .ToList();

                    qualifications = db.Qualifications.Where(x => trips.Contains((int)x.TripId)).ToList();
                }
                else
                {
                    //Passenger
                    var reservations = db.Reservations.Where(x => x.ApplicationUserId == userId)
                        .Select(x => x.ReservationId)
                        .ToList();

                    qualifications = db.Qualifications.Where(x => reservations.Contains((int)x.ReservationId)).ToList();
                }

                var totalStars = 0;

                //if user has been rated in less than 10 times, not enough data to star, return -1
                if (qualifications.Count < 10)
                {
                    return -1;
                }
                else
                {
                    foreach (var qualification in qualifications)
                    {
                        totalStars += qualification.Starts;
                    }

                    var average = totalStars / qualifications.Count();

                    return average;
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

                    var currentTime = Common.ConvertToUTCTime(DateTime.Now);

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

                    startDate = Common.ConvertToUTCTime(startDate);
                    endDate = Common.ConvertToUTCTime(endDate);

                    var currentTime = Common.ConvertToUTCTime(DateTime.Now);

                    if (user.UserType == Enums.UserType.Administrador)
                    {

                    }
                    else
                    {
                        trips = db.Trips.Where(x => x.Status == Enums.Status.Activo)
                        .Where(x => x.DateTime >= startDate && x.DateTime <= endDate)
                        .Where(x => fromCountyDistricts.Contains(x.FromTownId) && toCountyDistricts.Contains(x.ToTownId))
                        .Include(x => x.ApplicationUser)
                        .Where(x => x.DateTime > currentTime)
                        .ToList();
                    }
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
                    EmailHandler.SendErrorEmail(log.Line, log.Location, log.LogType, log.Message, log.Method, log.Timestamp, log.UserEmail, log.Fields, logo);
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

        public static void FinilizeExpiredTrips()
        {
            using (var db = new ApplicationDbContext())
            {
                var currentUTCTime = ConvertToUTCTime(DateTime.Now);
                var currentExpiredTrips = db.Trips
                    .Where(x => x.DateTime < currentUTCTime)
                    .Where(x => x.Status != Status.Finalizado)
                    .ToList();

                foreach (var expiredTrip in currentExpiredTrips)
                {
                    expiredTrip.Status = Status.Finalizado;

                    db.Entry(expiredTrip).State = EntityState.Modified;
                }

                db.SaveChanges();
            }
        }
    }
}