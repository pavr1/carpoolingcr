namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBalanceHistorialPromoAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BalanceHistorials", "PromoAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BalanceHistorials", "PromoAmount");
        }
    }
}
