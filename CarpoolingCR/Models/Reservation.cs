using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Models
{
    [DataContract(IsReference = true)]
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Espacios Solicitados")]
        public int RequestedSpaces { get; set; }
        [Required]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public ReservationStatus Status { get; set; }
        
        public string PassengerName { get; set; }

        public List<Qualification> Qualifications { get; set; }

        public bool IsPassengerQualified { get; set; }
        public bool IsDriverQualified { get; set; }
    }
}