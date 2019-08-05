using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class Qualification
    {
        public int QualificationId { get; set; }

        public string FromUserId { get; set; }
        public ApplicationUser FromUser { get; set; }

        public string ToUserId { get; set; }
        public ApplicationUser ToUser { get; set; }

        public int Starts { get; set; }
        public string Comments { get; set; }


        //public int QualificationId { get; set; }

        public string QualifierId { get; set; }
        public ApplicationUser Qualifier { get; set; }

        public int? ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int? TripId { get; set; }
        public Trip Trip { get; set; }
    }
}