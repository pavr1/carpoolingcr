namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedArrivalDateTimeintrips : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "ArrivalDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "ArrivalDateTime");
        }
    }
}
