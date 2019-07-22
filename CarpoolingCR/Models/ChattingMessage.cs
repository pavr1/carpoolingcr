using System;

namespace CarpoolingCR.Models
{
    public class ChattingMessage
    {
        public int ChattingMessageId { get; set; }
        public int ReservationId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}