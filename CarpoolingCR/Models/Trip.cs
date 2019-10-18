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
        private DateTime _ArrivalDateTime;

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

        public DateTime ArrivalDateTime
        {
            get
            {
                return _ArrivalDateTime;
            }
            set
            {
                if (value != DateTime.MinValue)
                {
                    _ArrivalDateTime = value;
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
        public string AproxDistance { get; set; }

        public virtual int FromTownId { get; set; }

        [NotMapped]
        public virtual District FromTown { get; set; }

        public virtual int ToTownId { get; set; }
        [NotMapped]
        public virtual District ToTown { get; set; }

        public virtual int RouteId { get; set; }
        [NotMapped]
        public virtual  District Route { get; set; }

        [NotMapped]
        public List<Reservation> Reservations { get; set; }
        [NotMapped]
        public string DateTimeStr
        {
            get
            {
                return LocalDateTime.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }
        [NotMapped]
        public double Date12AMUnix
        {
            get
            {
                var auxDate = new DateTime(LocalDateTime.Year, LocalDateTime.Month, LocalDateTime.Day, 12, 0, 0, DateTimeKind.Local);
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var unixDateTime = (auxDate.ToUniversalTime() - epoch).TotalSeconds;
                
                return unixDateTime;
            }
        }
        [NotMapped]
        public DateTime LocalDateTime
        {
            get
            {
                return Common.ConvertToLocalTime(_DateTime);
            }
        }

        [NotMapped]
        public DateTime LocalArrivalDateTime
        {
            get
            {
                return Common.ConvertToLocalTime(_ArrivalDateTime);
            }
        }

        public List<Qualification> Qualifications { get; set; }
    }
}