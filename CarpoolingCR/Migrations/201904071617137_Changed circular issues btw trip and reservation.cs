namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedcircularissuesbtwtripandreservation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "TripId", "dbo.Trips");
            DropIndex("dbo.Reservations", new[] { "TripId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Reservations", "TripId");
            AddForeignKey("dbo.Reservations", "TripId", "dbo.Trips", "TripId", cascadeDelete: true);
        }
    }
}
