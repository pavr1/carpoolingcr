using CarpoolingCR.Models;
using CarpoolingCR.Models.Promos;
using CarpoolingCR.Objects.Responses.Base;
using System.Collections.Generic;

namespace CarpoolingCR.Objects.Responses
{
    public class BlockingAmountResponse : BaseResponse
    {
        public string Type { get; set; }
        public List<BlockedAmount> Balances { get; set; }
        public List<BalanceHistorial> Historial { get; set; }
    }
}