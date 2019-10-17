namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAproxDistancetotrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "AproxDistance", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "AproxDistance");
        }
    }
}
