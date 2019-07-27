using CarpoolingCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Objects.Responses
{
    public class TripIndexResponse
    {
        public bool IsAdmin { get; set; }
        public List<Trip> Trips{ get; set; }
        public bool ReachedMaxCount { get; set; }
    }
}