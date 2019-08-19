using CarpoolingCR.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace CarpoolingCR.Models.Vehicle
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        public string ApplicationUserId { get; set; }
        [Required]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int SelectedBrandId { get; set; }
        [NotMapped]
        public Brand SelectedBrand { get; set; }

        [Required]
        public int SelectedModelId { get; set; }
        [NotMapped]
        public Model SelectedModel { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Plate { get; set; }
        public int Spaces { get; set; }

        [NotMapped]
        public List<Model> Models
        {
            get
            {
                return SelectedBrand.Models;
            }
        }
    }

    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }

        public virtual List<Model> Models { get; set; }
    }

    public class Model
    {
        public int ModelId { get; set; }
        public string Description { get; set; }
    }
}