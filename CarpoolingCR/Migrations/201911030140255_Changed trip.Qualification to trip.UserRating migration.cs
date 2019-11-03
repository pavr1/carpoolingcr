namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedtripQualificationtotripUserRatingmigration : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.UserRatings", "TripId");
            AddForeignKey("dbo.UserRatings", "TripId", "dbo.Trips", "TripId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRatings", "TripId", "dbo.Trips");
            DropIndex("dbo.UserRatings", new[] { "TripId" });
        }
    }
}
