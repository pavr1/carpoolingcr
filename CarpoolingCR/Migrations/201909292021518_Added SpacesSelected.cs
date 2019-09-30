namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSpacesSelected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "SpacesSelected", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "SpacesSelected");
        }
    }
}
