using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Provider;

namespace CarpoolingCR.Models.Promos
{
    public class UserPromos
    {
        public int UserPromosId { get; set; }
        public string UserId { get; set; }
        public virtual int PromoId { get; set; }
        public virtual Promo Promo { get; set; }
        public DateTime Date { get; set; }
        public int BlockedAmountId { get; set; }

        public ApplicationUser GetUser()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Users.Where(x => x.Id == UserId).Single();
            }
        }
    }
}