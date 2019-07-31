using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using CarpoolingCR.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarpoolingCR.Models
{
    [DataContract(IsReference = true)]
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Primer Apellido")]
        public string LastName { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string SecondLastName { get; set; }

        [Required]
        [Display(Name = "Teléfono #1")]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        [Display(Name = "Teléfono #2")]
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }

        [Display(Name = "Cuenta de Facebook")]
        public string FacebookAccount { get; set; }

        [Required]
        [Display(Name = "Tipo de Usuario")]
        public Enums.UserType UserType { get; set; }

        [Display(Name = "País")]
        public int? CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public Enums.ProfileStatus Status { get; set; }

        public int? BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        [NotMapped]
        public MonthlyBalance MonthlyBalance { get; set; }
        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public string MessageType { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim("Name", this.Name.ToString()));


            // Add custom user claims here
            return userIdentity;
        }
    }

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

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Country> Countries { get; set; }

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

        public System.Data.Entity.DbSet<CarpoolingCR.Models.Town> Towns { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.ChattingMessage> ChattingMessages { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.TripQuestionInfo> TripQuestionInfos { get; set; }

        public System.Data.Entity.DbSet<CarpoolingCR.Models.TripQuestion> TripQuestions { get; set; }
    }
}