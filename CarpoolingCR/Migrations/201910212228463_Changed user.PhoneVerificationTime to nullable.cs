namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeduserPhoneVerificationTimetonullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUsers", "PhoneVerificationTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUsers", "PhoneVerificationTime", c => c.DateTime(nullable: false));
        }
    }
}
