namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedfromtotownstostringtype : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "JourneyId", "dbo.Journeys");
            DropIndex("dbo.Trips", new[] { "JourneyId" });
            RenameColumn(table: "dbo.Trips", name: "JourneyId", newName: "Journey_JourneyId");
            AddColumn("dbo.Trips", "FromTown", c => c.String());
            AddColumn("dbo.Trips", "ToTown", c => c.String());
            AlterColumn("dbo.Trips", "Journey_JourneyId", c => c.Int());
            CreateIndex("dbo.Trips", "Journey_JourneyId");
            AddForeignKey("dbo.Trips", "Journey_JourneyId", "dbo.Journeys", "JourneyId");
            DropColumn("dbo.Trips", "FromTownId");
            DropColumn("dbo.Trips", "ToTownId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "ToTownId", c => c.Int(nullable: false));
            AddColumn("dbo.Trips", "FromTownId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Trips", "Journey_JourneyId", "dbo.Journeys");
            DropIndex("dbo.Trips", new[] { "Journey_JourneyId" });
            AlterColumn("dbo.Trips", "Journey_JourneyId", c => c.Int(nullable: false));
            DropColumn("dbo.Trips", "ToTown");
            DropColumn("dbo.Trips", "FromTown");
            RenameColumn(table: "dbo.Trips", name: "Journey_JourneyId", newName: "JourneyId");
            CreateIndex("dbo.Trips", "JourneyId");
            AddForeignKey("dbo.Trips", "JourneyId", "dbo.Journeys", "JourneyId", cascadeDelete: true);
        }
    }
}
