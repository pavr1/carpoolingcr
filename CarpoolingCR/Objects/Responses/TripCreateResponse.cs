using CarpoolingCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Objects.Responses
{
    public class TripCreateResponse
    {
        public List<LocationsResponse> Towns { get; set; }
        public Trip Trip { get; set; }
    }
}