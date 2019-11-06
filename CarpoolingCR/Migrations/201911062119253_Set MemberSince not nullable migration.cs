namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetMemberSincenotnullablemigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUsers", "MemberSince", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUsers", "MemberSince", c => c.DateTime());
        }
    }
}
