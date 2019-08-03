namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Severalchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "PassengerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "PassengerName");
        }
    }
}
