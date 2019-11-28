using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public int BankId { get; set; }
        [Display(Name = "Banco")]
        public Bank Bank { get; set; }

        public string UserId { get; set; }

        [Required]
        [Display(Name = "Cuenta de Ahorros")]
        public string SavingsAccount { get; set; }
        [Required]
        [Display(Name = "Cuenta Sinpe")]
        public string Sinpe { get; set; }
    }
}