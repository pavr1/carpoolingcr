using CarpoolingCR.Models;
using System.Collections.Generic;

namespace CarpoolingCR.Objects.Responses
{
    public class DisplayMessagesResponse
    {
        public string ActualUserId { get; set; }
        public List<TripQuestion> Messages { get; set; }
    }
}