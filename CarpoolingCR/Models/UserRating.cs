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
        public decimal Stars { get; set; }
        public string Comments { get; set; }
        public DateTime DateTime { get; set; }
        [NotMapped]
        public bool LoadUserInstances { get; set; }

        [NotMapped]
        public ApplicationUser FromUser
        {
            get
            {
                if (LoadUserInstances)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        return db.Users.Where(x => x.Id == FromId).SingleOrDefault();
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        [NotMapped]
        public ApplicationUser ToUser
        {
            get
            {
                if (LoadUserInstances)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        return db.Users.Where(x => x.Id == ToId).SingleOrDefault();
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}