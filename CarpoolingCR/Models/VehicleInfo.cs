using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class VehicleInfo
    {
        public int VehicleInfoId { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser  ApplicationUser { get; set; }
    }
}