using CarpoolingCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Objects.Responses
{
    public class ReservationIndexResponse
    {
        public List<Reservation> Reservations{ get; set; }
        public List<Town> Towns { get; set; }
    }
}