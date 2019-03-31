using CarpoolingCR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarpoolingCR
{
    public class DBConnection1: DbContext
    {
        public DBConnection1() : base("CarpoolingConnection")
        {

        }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Log> Logs { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Trip> Trips { get; set; }
        //public System.Data.Entity.DbSet<CarpoolingCR.Models.UserExtension> UserExtensions { get; set; }
    }
}