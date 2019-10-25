namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUserBirthday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "BirthDate");
        }
    }
}
