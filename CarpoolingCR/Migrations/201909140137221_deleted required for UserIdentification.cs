namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedrequiredforUserIdentification : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUsers", "UserIdentification", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUsers", "UserIdentification", c => c.String(nullable: false));
        }
    }
}
