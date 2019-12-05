namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtripIdandsetreservationId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlockedAmounts", "TripId", c => c.Int());
            AlterColumn("dbo.BlockedAmounts", "ReservationId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlockedAmounts", "ReservationId", c => c.Int(nullable: false));
            DropColumn("dbo.BlockedAmounts", "TripId");
        }
    }
}
