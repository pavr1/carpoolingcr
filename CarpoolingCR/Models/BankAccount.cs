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

        [Display(Name = "Banco")]
        public int BankId { get; set; }
        public Bank Bank { get; set; }

        [Required]
        [Display(Name = "Cuenta de Ahorros")]
        public string SavingsAccount { get; set; }
        [Required]
        [Display(Name = "Cuenta Sinpe")]
        public string Sinpe { get; set; }
    }
}