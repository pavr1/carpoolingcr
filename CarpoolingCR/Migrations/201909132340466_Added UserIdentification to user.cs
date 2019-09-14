namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserIdentificationtouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "UserIdentification", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "UserIdentification");
        }
    }
}
