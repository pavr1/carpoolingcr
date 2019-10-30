namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserRatings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRatings",
                c => new
                    {
                        UserRatingId = c.Int(nullable: false, identity: true),
                        TripId = c.Int(nullable: false),
                        FromId = c.String(),
                        ToId = c.String(),
                        Stars = c.Int(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.UserRatingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserRatings");
        }
    }
}
