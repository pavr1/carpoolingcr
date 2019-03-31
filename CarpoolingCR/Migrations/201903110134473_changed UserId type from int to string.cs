namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedUserIdtypefrominttostring : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reservations", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Reservations", "ApplicationUserId");
            RenameColumn(table: "dbo.Reservations", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.Reservations", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservations", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reservations", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Reservations", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Reservations", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Reservations", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "ApplicationUser_Id");
        }
    }
}
