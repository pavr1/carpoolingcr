namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSpacesPricesSelectedtoreservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "SpacesPricesSelected", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "SpacesPricesSelected");
        }
    }
}
