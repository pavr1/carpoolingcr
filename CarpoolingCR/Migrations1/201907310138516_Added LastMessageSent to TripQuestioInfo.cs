namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLastMessageSenttoTripQuestioInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TripQuestionInfoes", "LastMessageSent", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TripQuestionInfoes", "LastMessageSent");
        }
    }
}
