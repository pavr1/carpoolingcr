using System;
using System.Drawing;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Models
{
    public class RideTransaction
    {
        public int RideTransactionId { get; set; }
        public string UserId { get; set; }
        public DateTime RequestedDate { get; set; }
        public RideTransactionTypeEnum TransactionType { get; set; }
        public RideTransactionStatusEnum TransactionStatus { get; set; }

        public DateTime? AppliedDate { get; set; }
        public string ReferencedNumber { get; set; }
        public byte[] Image { get; set; }
        public string PurchasedBank { get; set; }
        public string PurchasedSavingsAccount { get; set; }
        public string PurchasedSinpeAccount { get; set; }
        public decimal Amount { get; set; }
        public string Detail { get; set; }
    }
}