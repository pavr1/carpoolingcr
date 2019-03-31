using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class MonthlyBalance
    {
        public int MonthlyBalanceId { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Mes")]
        public int Month { get; set; }
        [Required]
        [Display(Name = "Año")]
        public int Year { get; set; }


        public IList<MonthlyTransactions> MonthlyTransactions { get; set; }
    }
}