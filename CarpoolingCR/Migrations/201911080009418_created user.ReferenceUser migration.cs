namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createduserReferenceUsermigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "ReferencedUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "ReferencedUser");
        }
    }
}
