namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedChattingMessages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChattingMessages",
                c => new
                    {
                        ChattingMessageId = c.Int(nullable: false, identity: true),
                        ReservationId = c.Int(nullable: false),
                        FromName = c.String(),
                        ToName = c.String(),
                        Message = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChattingMessageId);
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        ChatMessageId = c.Int(nullable: false, identity: true),
                        ReservationId = c.Int(nullable: false),
                        FromName = c.String(),
                        ToName = c.String(),
                        Message = c.String(),
                        Date = c.DateTime(nullable: false),
                        Date2 = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChatMessageId);
            
            DropTable("dbo.ChattingMessages");
        }
    }
}
