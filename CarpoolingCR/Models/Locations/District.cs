using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models.Locations
{
    public class District
    {
        public int DistrictId { get; set; }
        public string Name { get; set; }

        public int CountyId { get; set; }
        public County County { get; set; }
    }
}