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
    }
}