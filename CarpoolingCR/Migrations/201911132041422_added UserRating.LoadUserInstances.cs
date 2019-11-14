namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserRatingLoadUserInstances : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRatings", "TripId", "dbo.Trips");
            DropIndex("dbo.UserRatings", new[] { "TripId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.UserRatings", "TripId");
            AddForeignKey("dbo.UserRatings", "TripId", "dbo.Trips", "TripId", cascadeDelete: true);
        }
    }
}
