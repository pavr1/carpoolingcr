using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public virtual int JourneyId { get; set; }
        [Required]
        [Display(Name = "Ruta")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public Enums.Status Status { get; set; }
    }
}