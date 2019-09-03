namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedNotificationRequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationRequests",
                c => new
                    {
                        NotificationRequestId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FromTownId = c.Int(nullable: false),
                        ToTownId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        RequestedFromDateTime = c.DateTime(nullable: false),
                        RequestedToDateTime = c.DateTime(nullable: false),
                        ReservationId = c.Int(),
                        Status = c.Int(nullable: false),
                        FromTown_DistrictId = c.Int(),
                        ToTown_DistrictId = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NotificationRequestId)
                .ForeignKey("dbo.Districts", t => t.FromTown_DistrictId)
                .ForeignKey("dbo.Reservations", t => t.ReservationId)
                .ForeignKey("dbo.Districts", t => t.ToTown_DistrictId)
                .ForeignKey("dbo.ApplicationUsers", t => t.User_Id)
                .Index(t => t.ReservationId)
                .Index(t => t.FromTown_DistrictId)
                .Index(t => t.ToTown_DistrictId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationRequests", "User_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.NotificationRequests", "ToTown_DistrictId", "dbo.Districts");
            DropForeignKey("dbo.NotificationRequests", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.NotificationRequests", "FromTown_DistrictId", "dbo.Districts");
            DropIndex("dbo.NotificationRequests", new[] { "User_Id" });
            DropIndex("dbo.NotificationRequests", new[] { "ToTown_DistrictId" });
            DropIndex("dbo.NotificationRequests", new[] { "FromTown_DistrictId" });
            DropIndex("dbo.NotificationRequests", new[] { "ReservationId" });
            DropTable("dbo.NotificationRequests");
        }
    }
}
