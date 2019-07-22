using CarpoolingCR.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Web;
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

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            origin = origin.AddSeconds(timestamp);

            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(origin, cstZone);

            return cstTime;
        }

        public static Int32 ConvertFromTimestampToUnix(DateTime date)
        {
            return (Int32)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}