using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public DateTime Timestamp{ get; set; }
        public string LogType { get; set; }
        //server/client
        public string Location { get; set; }
        public string Message { get; set; }
        public string Method { get; set; }
        public int Line { get; set; }

    }
}