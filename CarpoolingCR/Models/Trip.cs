using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

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
        public DateTime DateTime { get; set; }
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


        public string FromTown { get; set; }
        public string ToTown { get; set; }
    }
}