namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedChattingMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChattingMessages", "UserId", c => c.String());
            DropColumn("dbo.ChattingMessages", "FromName");
            DropColumn("dbo.ChattingMessages", "ToName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChattingMessages", "ToName", c => c.String());
            AddColumn("dbo.ChattingMessages", "FromName", c => c.String());
            DropColumn("dbo.ChattingMessages", "UserId");
        }
    }
}
