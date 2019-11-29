namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedBalanceHistorialCashAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BalanceHistorials", "RidecoinsAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BalanceHistorials", "CashAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.BalanceHistorials", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BalanceHistorials", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.BalanceHistorials", "CashAmount");
            DropColumn("dbo.BalanceHistorials", "RidecoinsAmount");
        }
    }
}
