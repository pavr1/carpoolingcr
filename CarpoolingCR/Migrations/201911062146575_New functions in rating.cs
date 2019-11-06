namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newfunctionsinrating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRatings", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRatings", "DateTime");
        }
    }
}
