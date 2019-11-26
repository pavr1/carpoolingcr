namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserPromosUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPromos", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPromos", "UserId");
        }
    }
}
