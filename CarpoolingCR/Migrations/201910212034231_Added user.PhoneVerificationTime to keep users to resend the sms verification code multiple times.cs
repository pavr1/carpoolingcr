namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddeduserPhoneVerificationTimetokeepuserstoresendthesmsverificationcodemultipletimes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "PhoneVerificationTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "PhoneVerificationTime");
        }
    }
}
