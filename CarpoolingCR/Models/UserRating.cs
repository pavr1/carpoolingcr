using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class UserRating
    {
        public int UserRatingId { get; set; }
        public int TripId { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public int Stars { get; set; }
        public string Comments { get; set; }
        public DateTime DateTime { get; set; }

        [NotMapped]
        public ApplicationUser FromUser
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.Users.Where(x => x.Id == FromId).SingleOrDefault();
                }
            }
        }

        [NotMapped]
        public ApplicationUser ToUser
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.Users.Where(x => x.Id == ToId).SingleOrDefault();
                }
            }
        }
    }
}