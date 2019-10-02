namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTripsReservationsandNotificationscounttouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "TripsCount", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationUsers", "ReservationsCount", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationUsers", "NotificationsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "NotificationsCount");
            DropColumn("dbo.ApplicationUsers", "ReservationsCount");
            DropColumn("dbo.ApplicationUsers", "TripsCount");
        }
    }
}
