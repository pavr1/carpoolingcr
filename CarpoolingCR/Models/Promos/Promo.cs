using CarpoolingCR.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Models.Promos
{
    public class Promo
    {
        public int PromoId { get; set; }

        public int PromoTypeId { get; set; }
        [Display(Name = "Tipo de Promo")]
        public PromoType PromoType { get; set; }

        [Display(Name = "Monto por usuario")]
        public decimal Amount { get; set; }
        [Display(Name = "Fecha Inicio")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Fecha Fin")]
        public DateTime? EndTime { get; set; }

        //if true, EndTime should be null
        [Display(Name = "Hasta agotar existencias")]
        public bool UntilAssignedAmountRunsOut { get; set; }
        [Display(Name = "Estado")]
        public PromoStatus Status { get; set; }
        //how many times a user can have this promo
        [Display(Name = "Veces por usuario")]
        public int MaxTimesPerUser { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        //set null when UntilAssignedAmountRanOut false
        [Display(Name = "Máximo para gastar")]
        public decimal? MaxAmountToSpend { get; set; }
        // = MaxAmountToSpend - user promo amounts spent
        [Display(Name = "Monto Disponible")]
        public decimal? AmountAvailable { get; set; }

        public List<UserPromos> UserPromos { get; set; }

        public DateTime LocalStartTime
        {
            get
            {
                return Common.ConvertToLocalTime(StartTime);
            }
        }

        public DateTime? LocalEndTime
        {
            get
            {
                if (EndTime != null)
                {
                    return Common.ConvertToLocalTime((DateTime)EndTime);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}