using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class TripQuestion
    {
        public int TripQuestionId { get; set; }

        public int TripQuestionInfoId { get; set; }
        public TripQuestionInfo TripQuestionInfo { get; set; }

        public string CurrentUserId { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }
}