namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdBlockedAmount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlockedAmounts",
                c => new
                    {
                        BlockedAmountId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ReservationId = c.Int(nullable: false),
                        BlockedBalanceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BlockedAmountId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BlockedAmounts");
        }
    }
}
