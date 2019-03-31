using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Models
{
    public class RideRequest
    {
        public int RideRequestId { get; set; }
        public string UserEmail{ get; set; }
        [Required]
        [Display(Name = "Trayecto")]
        public int JourneyId { get; set; }
        public string RouteDetails { get; set; }
        [Required]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Hora")]
        public DayPart DayPart { get; set; }
        public string Time { get; set; }
        [Required]
        [Display(Name = "Espacios Solicitados")]
        public int RequestedSpaces { get; set; }
        [Display(Name = "Estado")]
        public string Status { get; set; }
    }
}