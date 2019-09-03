using CarpoolingCR.Models.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class NotificationRequest
    {
        public int NotificationRequestId { get; set; }
        [Required]
        [Display(Name = "Pasajero")]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required]
        [Display(Name = "Origen")]
        public int FromTownId { get; set; }
        public District FromTown { get; set; }
        [Required]
        [Display(Name = "Destino")]
        public int ToTownId { get; set; }
        public District ToTown { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        [Display(Name = "Inicio")]
        public DateTime RequestedFromDateTime { get; set; }
        [Required]
        [Display(Name = "Fin")]
        public DateTime RequestedToDateTime { get; set; }
        [Display(Name = "Reservación")]
        public int? ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public RequestNotificationStatus Status { get; set; }
    }
}