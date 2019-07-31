using CarpoolingCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Objects.Responses
{
    public class TripDayTripsResponse
    {
        public string From { get; set; }
        public string To { get; set; }
        public string CurrentUserId { get; set; }

        public List<Trip> Trips { get; set; }

        public DateTime CurrentDate { get; set; }

        public List<Reservation> ExistentReservations { get; set; }
    }
}