using CarpoolingCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Objects.Responses
{
    public class JourneyResponse
    {
        public List<Country> Countries{ get; set; }
        public Country Country { get; set; }
    }
}