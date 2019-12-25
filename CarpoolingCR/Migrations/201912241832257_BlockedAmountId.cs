namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlockedAmountId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPromos", "BlockedAmountId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPromos", "BlockedAmountId");
        }
    }
}
