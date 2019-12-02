namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBlockedAmountPromoAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlockedAmounts", "PromoAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlockedAmounts", "PromoAmount");
        }
    }
}
