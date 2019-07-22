using CarpoolingCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Objects.Responses
{
    public class ChatMessageResponse: Base.BaseResponse
    {
        public List<ChattingMessage> Messages { get; set; }
        public string CurrentUserId { get; set; }
        public int ReservationId { get; set; }
    }
}