namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedJourneyfromTrip : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trips", "RouteDetail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "RouteDetail", c => c.String(nullable: false));
        }
    }
}
