using CarpoolingCR.Models;
using CarpoolingCR.Objects.Responses.Base;
using System.Collections.Generic;

namespace CarpoolingCR.Objects.Responses
{
    public class CancelNotificationResponse : BaseResponse
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public List<NotificationRequest> Notifications { get; set; }
    }
}