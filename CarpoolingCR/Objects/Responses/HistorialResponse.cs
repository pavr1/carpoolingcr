using CarpoolingCR.Models;
using CarpoolingCR.Objects.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Objects.Responses
{
    public class HistorialResponse: BaseResponse
    {
        public List<Trip> Trips { get; set; }
        public List<Reservation> Reservations { get; set; }

        public List<UserRating> UserRatings { get; set; }
    }
}