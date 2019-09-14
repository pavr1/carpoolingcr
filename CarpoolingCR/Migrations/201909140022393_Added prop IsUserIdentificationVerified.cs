namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedpropIsUserIdentificationVerified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "IsUserIdentificationVerified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "IsUserIdentificationVerified");
        }
    }
}
