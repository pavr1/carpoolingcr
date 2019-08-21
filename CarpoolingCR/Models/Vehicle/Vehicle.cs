using CarpoolingCR.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace CarpoolingCR.Models.Vehicle
{
    [DataContract(IsReference = true)]
    public class Vehicle
    {
        public int VehicleId { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        
        public int ModelId { get; set; }
        public virtual Model Model { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public int Spaces { get; set; }

        [NotMapped]
        public List<Model> Models
        {
            get
            {
                return Brand.Models;
            }
        }
    }

    [DataContract(IsReference = true)]
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }

        public virtual List<Model> Models {
            get {
                using (var db = new ApplicationDbContext())
                {
                    return db.Models.Where(x => x.BrandId == BrandId).ToList();
                }
            }
        }
    }

    [DataContract(IsReference = true)]
    public class Model
    {
        public int ModelId { get; set; }
        public string Description { get; set; }

        public int BrandId { get; set; }
        [NotMapped]
        public virtual Brand Brand { get; set; }
    }
}