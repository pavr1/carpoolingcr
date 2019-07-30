using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class TripQuestion
    {
        public int TripQuestionId { get; set; }
        public string FromId { get; set; }
        public string toId { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }
}