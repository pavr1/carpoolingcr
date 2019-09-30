namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Districtssetnotmapped : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "FromTown_DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Trips", "Route_DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Trips", "ToTown_DistrictId", "dbo.Districts");
            DropIndex("dbo.Trips", new[] { "FromTown_DistrictId" });
            DropIndex("dbo.Trips", new[] { "Route_DistrictId" });
            DropIndex("dbo.Trips", new[] { "ToTown_DistrictId" });
            DropColumn("dbo.Trips", "FromTown_DistrictId");
            DropColumn("dbo.Trips", "Route_DistrictId");
            DropColumn("dbo.Trips", "ToTown_DistrictId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "ToTown_DistrictId", c => c.Int());
            AddColumn("dbo.Trips", "Route_DistrictId", c => c.Int());
            AddColumn("dbo.Trips", "FromTown_DistrictId", c => c.Int());
            CreateIndex("dbo.Trips", "ToTown_DistrictId");
            CreateIndex("dbo.Trips", "Route_DistrictId");
            CreateIndex("dbo.Trips", "FromTown_DistrictId");
            AddForeignKey("dbo.Trips", "ToTown_DistrictId", "dbo.Districts", "DistrictId");
            AddForeignKey("dbo.Trips", "Route_DistrictId", "dbo.Districts", "DistrictId");
            AddForeignKey("dbo.Trips", "FromTown_DistrictId", "dbo.Districts", "DistrictId");
        }
    }
}
