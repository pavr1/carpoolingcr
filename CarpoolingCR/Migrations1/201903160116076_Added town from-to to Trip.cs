namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedtownfromtotoTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "FromTownId", c => c.Int(nullable: false));
            AddColumn("dbo.Trips", "ToTownId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "ToTownId");
            DropColumn("dbo.Trips", "FromTownId");
        }
    }
}
