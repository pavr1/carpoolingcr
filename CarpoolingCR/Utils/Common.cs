using CarpoolingCR.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CarpoolingCR.Utils
{
    public class Common
    {
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
                return db.Users.Where(x => x.Email == email).SingleOrDefault();
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

        public static void SendEmail(IdentityMessage msg)
        {
            new EmailService().SendAsync(msg);
        }

        public static void LogData(Log log)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Logs.Add(log);
                db.SaveChanges();
            }
        }

        public static DateTime ConvertToLocalTime(DateTime dateTime)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(WebConfigurationManager.AppSettings["CR_TimeZone"]);//Common.GetCurrentTimeZoneId());
            var utcDate = Convert.ToDateTime(dateTime);
            var localDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, timeZone);

            return localDate;
        }

        public static DateTime ConvertToUTCTime(DateTime dateTime)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(WebConfigurationManager.AppSettings["CR_TimeZone"]);//Common.GetCurrentTimeZoneId());
            var utcDate = Convert.ToDateTime(dateTime);
            var localDate = TimeZoneInfo.ConvertTimeToUtc(utcDate, timeZone);

            return localDate;
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
    }
}