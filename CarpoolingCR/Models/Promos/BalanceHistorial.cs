using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models.Promos
{
    public class BalanceHistorial
    {
        public int BalanceHistorialId { get; set; }
        //this should be null if the transaction is negative (trip money spent)
        public int? UserPromoId { get; set; }
        //this should be null if the transaction is positive (promo amount given)
        public int? TripId { get; set; }
        public string UserId { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public decimal RidecoinsAmount { get; set; }
        public decimal PromoAmount { get; set; }
        public decimal CashAmount { get; set; }
        
        //[NotMapped]
        //public ApplicationUser User
        //{
        //    get
        //    {
        //        using (var db = new ApplicationDbContext())
        //        {
        //            var user = db.Users.Where(x => x.Id == UserId).SingleOrDefault();

        //            foreach (var historial in user.BalanceHistorial)
        //            {
        //                historial.User = null;
        //            }
        //            return user;
        //        }
        //    }
        //}
    }
}