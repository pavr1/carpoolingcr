using CarpoolingCR.Models;
using System.Collections.Generic;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Objects.Responses
{
    public class TownIndexResponse
    {
        public UserType UserType { get; set; }
        public List<Town> Towns { get; set; }

        public Town Town { get; set; }
    }
}