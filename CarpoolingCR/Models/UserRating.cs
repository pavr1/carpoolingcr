using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class UserRating
    {
        public int UserRatingId { get; set; }
        public int TripId { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public int Stars { get; set; }
        public string Comments { get; set; }

    }
}