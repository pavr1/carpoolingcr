using CarpoolingCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Objects.Responses
{
    public class DriverTripHistorialResponse
    {
        //public bool IsPassenger { get; set; }
        //for admin and drivers
        public List<Trip> Trips { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}