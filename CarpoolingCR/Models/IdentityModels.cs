using CarpoolingCR.Models.Locations;
using CarpoolingCR.Models.Vehicle;
using CarpoolingCR.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CarpoolingCR.Models
{
    [DataContract(IsReference = true)]
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Cédula")]
        public string UserIdentification { get; set; }
        public bool IsUserIdentificationVerified { get; set; }
        public bool IsUserIdentificationInvalidated { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Primer Apellido")]
        public string LastName { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string SecondLastName { get; set; }

        [Required]
        [Display(Name = "Celular")]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }
        public int MobileVerficationNumber { get; set; }
        public bool IsPhoneVerified { get; set; }
        public DateTime? PhoneVerificationTime { get; set; }

        [Display(Name = "Cuenta de Facebook")]
        public string FacebookAccount { get; set; }

        [Required]
        [Display(Name = "Tipo de Usuario")]
        public Enums.UserType UserType { get; set; }

        [Display(Name = "País")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public DateTime? BirthDay { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public Enums.ProfileStatus Status { get; set; }
        public int? BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        public decimal PromoBalance { get; set; }
        [NotMapped]
        public MonthlyBalance MonthlyBalance { get; set; }
        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public string MessageType { get; set; }
        public string Picture { get; set; }
        public DateTime MemberSince { get; set; }
        public int? VehicleId { get; set; }
        public string ReferencedUser { get; set; }
        [NotMapped]
        public Vehicle.Vehicle Vehicle { get; set; }
        [NotMapped]
        public bool DontBreakLine { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return Name + " " + LastName;
            }
        }

        public int WaitingTimeToResendSMS
        {
            get
            {
                return Convert.ToInt32(WebConfigurationManager.AppSettings["WaitingTimeToResendSMS"]);
            }
        }

        [NotMapped]
        public int TripsAmount
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    if (UserType == Enums.UserType.Pasajero)
                    {
                        return -1;
                    }
                    else
                    {
                        return db.Trips.Where(x => x.ApplicationUserId == Id).Count();
                    }
                }
            }
        }

        [NotMapped]
        public int ReservationsAmount
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.Reservations.Where(x => x.ApplicationUserId == Id).Count();
                }
            }
        }

        [NotMapped]
        public List<UserRating> UserRatings
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    var ratings = db.UserRatings.Where(x => x.ToId == Id).ToList().ToList();

                    ratings.Sort((x, y) => y.DateTime.CompareTo(x.DateTime));

                    return ratings;
                }
            }
        }

        [NotMapped]
        public string LocalPhoneVerificationTimeStr
        {
            get
            {
                if (PhoneVerificationTime != null)
                {
                    return Common.ConvertToLocalTime((DateTime)PhoneVerificationTime).ToString(WebConfigurationManager.AppSettings["DateTimeUniversalFormat"]);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [NotMapped]
        public string ReferenceLink { get; set; }

        public decimal Stars
        {
            get
            {
                return Common.CalculateOverallUserStars(Id, (UserType == Enums.UserType.Conductor || UserType == Enums.UserType.Administrador));
            }
        }

        [NotMapped]
        public List<CarpoolingCR.Models.Promos.BalanceHistorial> BalanceHistorial
        {
            get {
                using (var db = new ApplicationDbContext())
                {
                    //by now do not filter by month
                    //var monthStartDate = DateTime.Now.ToLocalTime();
                    //monthStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0).ToLocalTime();
                    //var monthEndDate = DateTime.Now.ToLocalTime();
                    //monthEndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59).ToLocalTime();

                    //monthStartDate = Common.ConvertToUTCTime(monthStartDate);
                    //monthEndDate = Common.ConvertToUTCTime(monthEndDate);

                    var balances = db.BalanceHistorials.Where(x => x.UserId == Id).ToList();
                        //.Where(x => x.Date >= monthStartDate && x.Date >= monthEndDate).ToList();

                    balances.Sort((x, y) => y.Date.CompareTo(x.Date));

                    return balances;
                }
            }
        }

        [NotMapped]
        public decimal BlockedBalance
        {
            get
            {
                using (var db = new ApplicationDbContext())
                {
                    var balances = db.BlockedAmounts.Where(x => x.FromUserId == Id).ToList();
                    var blockedAmount = 0m;

                    foreach (var blocked in balances)
                    {
                        blockedAmount += blocked.BlockedBalanceAmount;
                    }

                    return blockedAmount;
                }
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            var trips = 0;
            var reservations = 0;
            var notifications = 0;

            using (var db = new ApplicationDbContext())
            {
                this.Country = db.Countries.Where(x => x.CountryId == this.CountryId).Single();

                if (this.UserType == Enums.UserType.Administrador)
                {
                    trips = db.Trips.Where(x => (x.Status == Enums.Status.Activo || x.Status == Enums.Status.Lleno || x.Status == Enums.Status.Pendiente)).Count();
                    reservations = db.Reservations.Where(x => (x.Status == Enums.ReservationStatus.Accepted || x.Status == Enums.ReservationStatus.Pending || x.Status == Enums.ReservationStatus.Rejected)).Count();
                    notifications = db.NotificationRequests.Where(x => (x.Status == CarpoolingCR.Utils.Enums.RequestNotificationStatus.Active)).Count();
                }
                else
                {
                    trips = db.Trips.Where(x => x.ApplicationUserId == this.Id && (x.Status == Enums.Status.Activo || x.Status == Enums.Status.Lleno || x.Status == Enums.Status.Pendiente)).Count();
                    reservations = db.Reservations.Where(x => x.ApplicationUserId == this.Id && (x.Status == Enums.ReservationStatus.Accepted || x.Status == Enums.ReservationStatus.Pending || x.Status == Enums.ReservationStatus.Rejected)).Count();
                    notifications = db.NotificationRequests.Where(x => x.UserId == this.Id && (x.Status == CarpoolingCR.Utils.Enums.RequestNotificationStatus.Active)).Count();
                }
            }

            userIdentity.AddClaim(new Claim("Name", this.Name.ToString()));
            userIdentity.AddClaim(new Claim("CountryCode", this.Country.CountryCode));
            userIdentity.AddClaim(new Claim("CurrencyChar", this.Country.CurrencyChar));
            userIdentity.AddClaim(new Claim("Picture", this.Picture));
            userIdentity.AddClaim(new Claim("UserType", Convert.ToInt32(this.UserType).ToString()));
            userIdentity.AddClaim(new Claim("Trips", trips.ToString()));
            userIdentity.AddClaim(new Claim("Reservations", reservations.ToString()));
            userIdentity.AddClaim(new Claim("Notifications", notifications.ToString()));

            // Add custom user claims here
            return userIdentity;
        }

        [NotMapped]
        public SelectList Brands
        {
            get
            {
                var brands = Common.GetAllVehicleBrandsAndModels();

                return new SelectList(brands, "BrandId", "Name");
            }
        }

        [NotMapped]
        public List<Brand> BrandsJson
        {
            get
            {
                return Common.GetAllVehicleBrandsAndModels();
            }
        }
    }
    //public class CustomUser: ApplicationUser
    //{
    //}

    //public static class IdentityExtensions
    //{
    //    public static string GetName(this IIdentity identity)
    //    {
    //        var claim = ((ClaimsIdentity)identity).FindFirst("Name");
    //        // Test for null to avoid issues during local testing
    //        return (claim != null) ? claim.Value : string.Empty;
    //    }
    //}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CarpoolingConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserExtension>().ToTable("UserExtension");


            modelBuilder.Entity<IdentityUserRole>()
                .HasKey(r => new { r.UserId, r.RoleId })
                .ToTable("AspNetUserRoles");

            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("AspNetUserLogins");
        }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Locations.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Log> Logs { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Trip> Trips { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Route> Routes { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Reservation> Reservations { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.RideRequest> RideRequests { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Bank> Banks { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.MonthlyBalance> MonthlyBalances { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.MonthlyTransactions> MonthlyTransactions { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.BankAccount> BankAccounts { get; set; }

        //public System.Data.Entity.DbSet<CarpoolingCR.Models.Town> Towns { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.ChattingMessage> ChattingMessages { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.TripQuestionInfo> TripQuestionInfos { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.TripQuestion> TripQuestions { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Locations.District> Districts { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Locations.County> Counties { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Locations.Province> Provinces { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Qualification> Qualifications { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.ProfileQualification> ProfileQualifications { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Vehicle.Vehicle> Vehicles { get; set; }
        public System.Data.Entity.DbSet<CarpoolingCR.Models.Vehicle.Brand> Brands { get; set; }
        public System.Data.Entity.DbSet<CarpoolingCR.Models.Vehicle.Model> Models { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.NotificationRequest> NotificationRequests { get; set; }
        public System.Data.Entity.DbSet<CarpoolingCR.Models.Phone> Phones { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.UserRating> UserRatings { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Promos.BalanceHistorial> BalanceHistorials { get; set; }
        public System.Data.Entity.DbSet<CarpoolingCR.Models.Promos.Promo> Promo { get; set; }
        public System.Data.Entity.DbSet<CarpoolingCR.Models.Promos.PromoType> PromoType { get; set; }
        public System.Data.Entity.DbSet<CarpoolingCR.Models.Promos.UserPromos> UserPromos { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Promos.BlockedAmount> BlockedAmounts { get; set; }
    }
}