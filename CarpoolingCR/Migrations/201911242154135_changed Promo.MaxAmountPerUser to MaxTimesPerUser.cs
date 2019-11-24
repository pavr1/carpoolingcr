namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedPromoMaxAmountPerUsertoMaxTimesPerUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promoes", "MaxTimesPerUser", c => c.Int(nullable: false));
            DropColumn("dbo.Promoes", "MaxAmountPerUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Promoes", "MaxAmountPerUser", c => c.Int(nullable: false));
            DropColumn("dbo.Promoes", "MaxTimesPerUser");
        }
    }
}
