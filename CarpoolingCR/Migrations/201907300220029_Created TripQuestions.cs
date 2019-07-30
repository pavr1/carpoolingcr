namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedTripQuestions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TripQuestions",
                c => new
                    {
                        TripQuestionId = c.Int(nullable: false, identity: true),
                        FromId = c.String(),
                        toId = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.TripQuestionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TripQuestions");
        }
    }
}
