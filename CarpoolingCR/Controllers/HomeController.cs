﻿using CarpoolingCR.Models;
using CarpoolingCR.Objects.Responses;
using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            //try
            //{
            //    //SMSHandler.SendSMS("+506", "8844-3317", "Mensaje de prueba de envío SMS", "https:///buscoridecr.com");

            //    var user = Common.GetUserByEmail(User.Identity.Name);

            //    if (user != null)
            //    {
            //        Common.UpdateUserTripsReservationsAndNotifications(user.Id);
            //    }

            //    if (!Common.IsAuthorized(User))
            //    {
            //        var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

            //        if (send)
            //        {
            //            EmailHandler.HomePageHit(logo);
            //        }
            //    }

            //    return View();
            //}
            //catch (Exception ex)
            //{
            //    var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

            //    Common.LogData(new Log
            //    {
            //        Line = Common.GetCurrentLine(),
            //        Location = Enums.LogLocation.Server,
            //        LogType = Enums.LogType.Error,
            //        Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
            //        Method = Common.GetCurrentMethod(),
            //        Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
            //        UserEmail = User.Identity.Name
            //    }, logo);

            //    ViewBag.Error = "¡Error inesperado, intente de nuevo!";

            //    return View();
            //}
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            try
            {
                var passengerReservations = new List<Reservation>();
                var driverTrips = new List<Trip>();
                var user = Common.GetUserByEmail(User.Identity.Name);

                var districtsSelectHtml = Common.GetLocationsStrings(1);

                List<Trip> trips = new List<Trip>();

                ReservationTransportationResponse response = new ReservationTransportationResponse
                {
                    Trips = new List<Trip>(),
                    PassengerReservations = new List<Reservation>(),
                    DriverTrips = new List<Trip>(),
                    SelectedJourneyId = -1,
                    SelectedRouteIndex = -1,
                    CurrentUserType = Enums.UserType.Pasajero,
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}