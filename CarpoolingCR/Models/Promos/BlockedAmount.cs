using System.Linq;

namespace CarpoolingCR.Models.Promos
{
    public class BlockedAmount
    {
        public int BlockedAmountId { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        //if blocked account is by a reservation creation
        public int? ReservationId { get; set; }
        //if blocked amount is by a trip creation
        public int? TripId { get; set; }
        public decimal BlockedBalanceAmount { get; set; }
        public string Detail { get; set; }
        public decimal PromoAmount { get; set; }
        public int? PromoId { get; set; }

        public ApplicationUser FromUser
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = db.Users.Where(x => x.Id == FromUserId).Single();

                    return user;
                }
            }
        }

        public ApplicationUser ToUser
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = db.Users.Where(x => x.Id == ToUserId).Single();

                    return user;
                }
            }
        }

        public Reservation Reservation
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    var reservation = db.Reservations.Where(x => x.ReservationId == ReservationId).Single();
                    reservation.Trip = db.Trips.Where(x => x.TripId == reservation.TripId).Single();
                    reservation.Trip.FromTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.FromTownId).Single();
                    reservation.Trip.ToTown = db.Districts.Where(x => x.DistrictId == reservation.Trip.ToTownId).Single();

                    return reservation;
                }
            }
        }

        public Trip Trip
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    var trip = db.Trips.Where(x => x.TripId == TripId).Single();
                    trip.FromTown = db.Districts.Where(x => x.DistrictId == trip.FromTownId).Single();
                    trip.ToTown = db.Districts.Where(x => x.DistrictId == trip.ToTownId).Single();

                    return trip;
                }
            }
        }
    }
}