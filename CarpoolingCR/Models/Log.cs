using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public string UserEmail { get; set; }
        public DateTime Timestamp{ get; set; }
        public LogType LogType { get; set; }
        //server/client
        public LogLocation Location { get; set; }
        public string Message { get; set; }
        public string Method { get; set; }
        public int Line { get; set; }
        public string Fields { get; set; }

    }
}