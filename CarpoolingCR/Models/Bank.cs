using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        [Required]
        [Display(Name = "Banco")]
        public string BankName { get; set; }
    }
}