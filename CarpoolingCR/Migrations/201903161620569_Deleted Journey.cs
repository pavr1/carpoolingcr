namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedJourney : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Journeys", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Routes", "JourneyId", "dbo.Journeys");
            DropForeignKey("dbo.Trips", "Journey_JourneyId", "dbo.Journeys");
            DropForeignKey("dbo.RideRequests", "JourneyId", "dbo.Journeys");
            DropIndex("dbo.Journeys", new[] { "CountryId" });
            DropIndex("dbo.Routes", new[] { "JourneyId" });
            DropIndex("dbo.Trips", new[] { "Journey_JourneyId" });
            DropIndex("dbo.RideRequests", new[] { "JourneyId" });
            DropColumn("dbo.Trips", "Journey_JourneyId");
            DropTable("dbo.Journeys");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Journeys",
                c => new
                    {
                        JourneyId = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Option1 = c.String(nullable: false),
                        Option2 = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JourneyId);
            
            AddColumn("dbo.Trips", "Journey_JourneyId", c => c.Int());
            CreateIndex("dbo.RideRequests", "JourneyId");
            CreateIndex("dbo.Trips", "Journey_JourneyId");
            CreateIndex("dbo.Routes", "JourneyId");
            CreateIndex("dbo.Journeys", "CountryId");
            AddForeignKey("dbo.RideRequests", "JourneyId", "dbo.Journeys", "JourneyId", cascadeDelete: true);
            AddForeignKey("dbo.Trips", "Journey_JourneyId", "dbo.Journeys", "JourneyId");
            AddForeignKey("dbo.Routes", "JourneyId", "dbo.Journeys", "JourneyId", cascadeDelete: true);
            AddForeignKey("dbo.Journeys", "CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
        }
    }
}
