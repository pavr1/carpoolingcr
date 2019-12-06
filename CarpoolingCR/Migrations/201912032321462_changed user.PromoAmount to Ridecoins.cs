namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeduserPromoAmounttoRidecoins : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "Ridecoins", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ApplicationUsers", "PromoBalance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "PromoBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ApplicationUsers", "Ridecoins");
        }
    }
}
