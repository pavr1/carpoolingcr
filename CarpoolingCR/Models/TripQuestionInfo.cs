using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class TripQuestionInfo
    {
        public int TripQuestionInfoId { get; set; }
        public string DriverId { get; set; }
        public string PassengerId { get; set; }

        [NotMapped]
        public ApplicationUser Driver { get; set; }
        [NotMapped]
        public ApplicationUser Passenger { get; set; }

        public List<TripQuestion> TripQuestions { get; set; }
    }
}