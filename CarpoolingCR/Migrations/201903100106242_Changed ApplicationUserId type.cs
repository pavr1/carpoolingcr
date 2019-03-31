namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedApplicationUserIdtype : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Trips", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Trips", "ApplicationUserId");
            RenameColumn(table: "dbo.Trips", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.Trips", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Trips", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Trips", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Trips", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Trips", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Trips", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trips", "ApplicationUser_Id");
        }
    }
}
