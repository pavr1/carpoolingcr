using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models.Promos
{
    public class BlockedAmount
    {
        public int BlockedAmountId { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public int ReservationId { get; set; }
        public decimal BlockedBalanceAmount { get; set; }
    }
}