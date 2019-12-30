using CarpoolingCR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CarpoolingCR.Objects.Responses
{
    public class TripDayTripsResponse
    {
        public string From { get; set; }
        public string To { get; set; }
        public string CurrentUserId { get; set; }

        public List<Trip> Trips { get; set; }

        public DateTime CurrentDate { get; set; }

        public List<Reservation> ExistentReservations { get; set; }
        public bool CouldNotFindExactTrip { get; set; }
        public string Currency { get; set; }

        public ApplicationUser CurrentUser
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = db.Users.Where(x => x.Id == CurrentUserId).Single();

                    return user;
                }
            }
        }
    }
}