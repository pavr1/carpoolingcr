namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedfromandtodistrictstotrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "FromTownId", c => c.Int(nullable: false));
            AddColumn("dbo.Trips", "ToTownId", c => c.Int(nullable: false));
            AddColumn("dbo.Trips", "FromTown_DistrictId", c => c.Int());
            AddColumn("dbo.Trips", "ToTown_DistrictId", c => c.Int());
            CreateIndex("dbo.Trips", "FromTown_DistrictId");
            CreateIndex("dbo.Trips", "ToTown_DistrictId");
            AddForeignKey("dbo.Trips", "FromTown_DistrictId", "dbo.Districts", "DistrictId");
            AddForeignKey("dbo.Trips", "ToTown_DistrictId", "dbo.Districts", "DistrictId");
            DropColumn("dbo.Trips", "FromTown");
            DropColumn("dbo.Trips", "ToTown");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "ToTown", c => c.Int(nullable: false));
            AddColumn("dbo.Trips", "FromTown", c => c.Int(nullable: false));
            DropForeignKey("dbo.Trips", "ToTown_DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Trips", "FromTown_DistrictId", "dbo.Districts");
            DropIndex("dbo.Trips", new[] { "ToTown_DistrictId" });
            DropIndex("dbo.Trips", new[] { "FromTown_DistrictId" });
            DropColumn("dbo.Trips", "ToTown_DistrictId");
            DropColumn("dbo.Trips", "FromTown_DistrictId");
            DropColumn("dbo.Trips", "ToTownId");
            DropColumn("dbo.Trips", "FromTownId");
        }
    }
}
