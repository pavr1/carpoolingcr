namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedreservationstatustobeReservationStatusenum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Status", c => c.String(nullable: false));
        }
    }
}
