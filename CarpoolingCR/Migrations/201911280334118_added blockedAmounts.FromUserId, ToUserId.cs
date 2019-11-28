namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedblockedAmountsFromUserIdToUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlockedAmounts", "FromUserId", c => c.String());
            AddColumn("dbo.BlockedAmounts", "ToUserId", c => c.String());
            DropColumn("dbo.BlockedAmounts", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlockedAmounts", "UserId", c => c.String());
            DropColumn("dbo.BlockedAmounts", "ToUserId");
            DropColumn("dbo.BlockedAmounts", "FromUserId");
        }
    }
}
