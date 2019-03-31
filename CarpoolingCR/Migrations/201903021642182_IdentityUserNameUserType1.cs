namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityUserNameUserType1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApplicationUsers", "UserType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "UserType", c => c.String());
        }
    }
}
