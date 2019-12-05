namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedblockedAmountPromoId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlockedAmounts", "PromoId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlockedAmounts", "PromoId");
        }
    }
}
