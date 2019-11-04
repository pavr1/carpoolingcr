namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddeduserMemberSincefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "MemberSince", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "MemberSince");
        }
    }
}
