namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deletedtripreservationandnotificationcountsfromusersincetheyarenotreallynecessary : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApplicationUsers", "TripsCount");
            DropColumn("dbo.ApplicationUsers", "ReservationsCount");
            DropColumn("dbo.ApplicationUsers", "NotificationsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "NotificationsCount", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationUsers", "ReservationsCount", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationUsers", "TripsCount", c => c.Int(nullable: false));
        }
    }
}
