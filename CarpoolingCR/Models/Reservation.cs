using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
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
        public string Status { get; set; }

        [NotMapped]
        public string PassengerName { get; set; }
    }
}