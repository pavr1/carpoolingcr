namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedTripQuestionInfomodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TripQuestionInfoes",
                c => new
                    {
                        TripQuestionInfoId = c.Int(nullable: false, identity: true),
                        DriverId = c.String(),
                        PassengerId = c.String(),
                    })
                .PrimaryKey(t => t.TripQuestionInfoId);
            
            AddColumn("dbo.TripQuestions", "TripQuestionInfoId", c => c.Int(nullable: false));
            AddColumn("dbo.TripQuestions", "IsDriverMessage", c => c.Boolean(nullable: false));
            CreateIndex("dbo.TripQuestions", "TripQuestionInfoId");
            AddForeignKey("dbo.TripQuestions", "TripQuestionInfoId", "dbo.TripQuestionInfoes", "TripQuestionInfoId", cascadeDelete: true);
            DropColumn("dbo.TripQuestions", "FromId");
            DropColumn("dbo.TripQuestions", "toId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TripQuestions", "toId", c => c.String());
            AddColumn("dbo.TripQuestions", "FromId", c => c.String());
            DropForeignKey("dbo.TripQuestions", "TripQuestionInfoId", "dbo.TripQuestionInfoes");
            DropIndex("dbo.TripQuestions", new[] { "TripQuestionInfoId" });
            DropColumn("dbo.TripQuestions", "IsDriverMessage");
            DropColumn("dbo.TripQuestions", "TripQuestionInfoId");
            DropTable("dbo.TripQuestionInfoes");
        }
    }
}
