namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTripQuestionInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TripQuestions", "CurrentUserId", c => c.String());
            DropColumn("dbo.TripQuestions", "IsDriverMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TripQuestions", "IsDriverMessage", c => c.Boolean(nullable: false));
            DropColumn("dbo.TripQuestions", "CurrentUserId");
        }
    }
}
