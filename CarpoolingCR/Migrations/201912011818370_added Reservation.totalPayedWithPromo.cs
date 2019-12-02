namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedReservationtotalPayedWithPromo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "totalPayedWithPromo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "totalPayedWithPromo");
        }
    }
}
