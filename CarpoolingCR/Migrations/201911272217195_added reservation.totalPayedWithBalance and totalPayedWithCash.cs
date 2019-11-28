namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedreservationtotalPayedWithBalanceandtotalPayedWithCash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "totalPayedWithBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Reservations", "totalPayedWithCash", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "totalPayedWithCash");
            DropColumn("dbo.Reservations", "totalPayedWithBalance");
        }
    }
}
