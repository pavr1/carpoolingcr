namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedpurchasedbankinformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RideTransactions", "PurchasedBank", c => c.String());
            AddColumn("dbo.RideTransactions", "PurchasedSavingsAccount", c => c.String());
            AddColumn("dbo.RideTransactions", "PurchasedSinpeAccount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RideTransactions", "PurchasedSinpeAccount");
            DropColumn("dbo.RideTransactions", "PurchasedSavingsAccount");
            DropColumn("dbo.RideTransactions", "PurchasedBank");
        }
    }
}
