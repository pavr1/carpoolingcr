namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedblokedAmountDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlockedAmounts", "Detail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlockedAmounts", "Detail");
        }
    }
}
