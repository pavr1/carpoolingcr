namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Phoneverifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "MobileVerficationNumber", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationUsers", "IsPhoneVerified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "IsPhoneVerified");
            DropColumn("dbo.ApplicationUsers", "MobileVerficationNumber");
        }
    }
}
