namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedLogModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "UserEmail", c => c.String());
            AlterColumn("dbo.Logs", "LogType", c => c.Int(nullable: false));
            AlterColumn("dbo.Logs", "Location", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logs", "Location", c => c.String());
            AlterColumn("dbo.Logs", "LogType", c => c.String());
            DropColumn("dbo.Logs", "UserEmail");
        }
    }
}
