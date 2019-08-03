namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bigchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Trips", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Reservations", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ApplicationUsers", "Name", c => c.String(nullable: false));
            AddColumn("dbo.ApplicationUsers", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.ApplicationUsers", "SecondLastName", c => c.String());
            AddColumn("dbo.ApplicationUsers", "Phone1", c => c.String(nullable: false));
            AddColumn("dbo.ApplicationUsers", "Phone2", c => c.String());
            AddColumn("dbo.ApplicationUsers", "FacebookAccount", c => c.String());
            AddColumn("dbo.ApplicationUsers", "UserType", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationUsers", "CountryId", c => c.Int(nullable: true));
            AddColumn("dbo.ApplicationUsers", "Status", c => c.Int(nullable: false));
            CreateIndex("dbo.Trips", "ApplicationUser_Id");
            CreateIndex("dbo.ApplicationUsers", "CountryId");
            CreateIndex("dbo.Reservations", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUsers", "CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
            AddForeignKey("dbo.Trips", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.Reservations", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
            DropColumn("dbo.Trips", "UserEmail");
            DropColumn("dbo.Reservations", "UserEmail");
            DropTable("dbo.UserExtension");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserExtension",
                c => new
                    {
                        UserExtensionId = c.Int(nullable: false, identity: true),
                        AspNetUserId = c.String(),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        SecondLastName = c.String(),
                        Phone1 = c.String(nullable: false),
                        Phone2 = c.String(),
                        FacebookAccount = c.String(),
                        UserType = c.String(nullable: false),
                        CountryId = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserExtensionId);
            
            AddColumn("dbo.Reservations", "UserEmail", c => c.String());
            AddColumn("dbo.Trips", "UserEmail", c => c.String());
            DropForeignKey("dbo.Reservations", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Trips", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUsers", "CountryId", "dbo.Countries");
            DropIndex("dbo.Reservations", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUsers", new[] { "CountryId" });
            DropIndex("dbo.Trips", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.ApplicationUsers", "Status");
            DropColumn("dbo.ApplicationUsers", "CountryId");
            DropColumn("dbo.ApplicationUsers", "UserType");
            DropColumn("dbo.ApplicationUsers", "FacebookAccount");
            DropColumn("dbo.ApplicationUsers", "Phone2");
            DropColumn("dbo.ApplicationUsers", "Phone1");
            DropColumn("dbo.ApplicationUsers", "SecondLastName");
            DropColumn("dbo.ApplicationUsers", "LastName");
            DropColumn("dbo.ApplicationUsers", "Name");
            DropColumn("dbo.Reservations", "ApplicationUser_Id");
            DropColumn("dbo.Reservations", "ApplicationUserId");
            DropColumn("dbo.Trips", "ApplicationUser_Id");
            DropColumn("dbo.Trips", "ApplicationUserId");
        }
    }
}
