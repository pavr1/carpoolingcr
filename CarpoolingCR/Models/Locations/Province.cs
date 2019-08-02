using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models.Locations
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public string Name { get; set; }
        public List<County> Counties { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}