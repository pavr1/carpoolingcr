using CarpoolingCR.Models;
using CarpoolingCR.Objects.Responses.Base;
using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarpoolingCR.Objects.Responses
{
    public class ReservationTransportationResponse : BaseResponse
    {
        public List<Trip> Trips { get; set; }
        public List<Reservation> PassengerReservations { get; set; }
        public List<Trip> DriverTrips { get; set; }

        public int SelectedJourneyId { get; set; }
        public int SelectedRouteIndex { get; set; }

        public Enums.UserType CurrentUserType { get; set; }

        public List<Town> Towns { get; set; }

        public string From { get; set; }
        public string To { get; set; }
        public int TabIndex { get; set; }
    }
}