using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarpoolingCR.Objects.Responses
{
    public class UserManageResponse
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string UserType { get; set; }
        public string Status { get; set; }
    }
}