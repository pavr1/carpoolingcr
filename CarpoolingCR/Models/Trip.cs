using CarpoolingCR.Models.Locations;
using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CarpoolingCR.Models
{
    [DataContract(IsReference = true)]
    public class Trip
    {
        public int TripId { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        private DateTime _DateTime;

        public DateTime DateTime
        {
            get
            {
                return _DateTime;
            }
            set
            {
                if (value != DateTime.MinValue)
                {
                    _DateTime = value;
                }
            }
        }

        [Required]
        [Display(Name = "Total de espacios")]
        [Range(0, 10)]
        public int TotalSpaces { get; set; }
        [Required]
        [Display(Name = "Espacios Disponibles")]
        [Range(0, 10)]
        public int AvailableSpaces { get; set; }
        [Required]
        [Display(Name = "Cuota")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public Enums.Status Status { get; set; }
        [Display(Name = "Fecha Creación")]
        public DateTime CreatedTime { get; set; }
        [Display(Name = "Detalles")]
        public string Details { get; set; }


        public int FromTownId { get; set; }
        public District FromTown { get; set; }

        public int ToTownId { get; set; }
        public District ToTown { get; set; }

        [NotMapped]
        public List<Reservation> Reservations { get; set; }
        [NotMapped]
        public string DateTimeStr
        {
            get
            {
                return _DateTime.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }

        public List<Qualification> Qualifications { get; set; }
    }
}