using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class ProfileQualification
    {
        public int ProfileQualificationId { get; set; }

        public string Userid { get; set; }
        public ApplicationUser User { get; set; }

        public List<Qualification> Qualifications { get; set; }
    }
}