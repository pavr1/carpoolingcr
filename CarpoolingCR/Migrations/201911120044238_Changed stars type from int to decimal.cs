namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedstarstypefrominttodecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserRatings", "Stars", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserRatings", "Stars", c => c.Int(nullable: false));
        }
    }
}
