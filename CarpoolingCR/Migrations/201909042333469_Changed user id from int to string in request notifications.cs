namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeduseridfrominttostringinrequestnotifications : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotificationRequests", "User_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.NotificationRequests", new[] { "User_Id" });
            DropColumn("dbo.NotificationRequests", "UserId");
            RenameColumn(table: "dbo.NotificationRequests", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.NotificationRequests", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.NotificationRequests", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.NotificationRequests", "UserId");
            AddForeignKey("dbo.NotificationRequests", "UserId", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationRequests", "UserId", "dbo.ApplicationUsers");
            DropIndex("dbo.NotificationRequests", new[] { "UserId" });
            AlterColumn("dbo.NotificationRequests", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.NotificationRequests", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.NotificationRequests", name: "UserId", newName: "User_Id");
            AddColumn("dbo.NotificationRequests", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.NotificationRequests", "User_Id");
            AddForeignKey("dbo.NotificationRequests", "User_Id", "dbo.ApplicationUsers", "Id");
        }
    }
}
