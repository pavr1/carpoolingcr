using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models.Locations
{
    public class Country
    {
        public int CountryId { get; set; }
        [Required]
        [Display(Name = "País")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public Enums.Status Status { get; set; }
        
        public virtual ICollection<Province> Provinces { get; set; }
    }
}