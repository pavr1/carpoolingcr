using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models.Locations
{
    public class County
    {
        public int CountyId { get; set; }
        public string Name { get; set; }

        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        public List<District> Districts { get; set; }
    }
}