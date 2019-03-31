using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class MonthlyTransactions
    {
        public int MonthlyTransactionsId { get; set; }

        public int MonthlyBalanceId { get; set; }
        public MonthlyBalance MonthlyBalance { get; set; }

        [Display(Name = "Balance Inicial")]
        [DataType(DataType.Currency)]
        public decimal InitialBalance { get; set; }

        [Display(Name = "Monto de Crédito")]
        [DataType(DataType.Currency)]
        public decimal CreditAmount { get; set; }

        [Display(Name = "Tipo de Crédito")]
        public Enums.CreditTypes CreditType { get; set; }

        [Display(Name = "Referncia de Crédito")]
        public int? CreditReference { get; set; }


        [Display(Name = "Monto de Débito")]
        [DataType(DataType.Currency)]
        public decimal DebitAmount { get; set; }

        [Display(Name = "Tipo de Débito")]
        public Enums.DebitTypes DebitType { get; set; }

        [Display(Name = "Referencia de Débito")]
        public int? DebitReference { get; set; }

        [Display(Name = "Balance Final")]
        [DataType(DataType.Currency)]
        public decimal FinalBalance { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Currency)]
        public DateTime Date { get; set; }
    }
}