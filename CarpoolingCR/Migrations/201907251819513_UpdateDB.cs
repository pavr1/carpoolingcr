namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Fields", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "Fields");
        }
    }
}
