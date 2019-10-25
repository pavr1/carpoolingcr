namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddeduserBirthday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "BirthDay", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "BirthDay");
        }
    }
}
