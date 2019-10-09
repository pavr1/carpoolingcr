using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Objects.Responses.Base
{
    public class BaseResponse
    {
        public string Message { get; set; }
        public string MessageType { get; set; }
        public string Html { get; set; }
    }
}