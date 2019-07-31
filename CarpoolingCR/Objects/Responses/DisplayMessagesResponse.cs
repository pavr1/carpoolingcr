using CarpoolingCR.Models;
using System.Collections.Generic;

namespace CarpoolingCR.Objects.Responses
{
    public class DisplayMessagesResponse
    {
        public string ActualUserId { get; set; }
        public List<TripQuestionInfo> QuestionsInfo { get; set; }
    }
}