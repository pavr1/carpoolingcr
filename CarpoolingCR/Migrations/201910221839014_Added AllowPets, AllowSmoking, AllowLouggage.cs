namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAllowPetsAllowSmokingAllowLouggage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "AllowPets", c => c.Boolean(nullable: false));
            AddColumn("dbo.Trips", "AllowSmoking", c => c.Boolean(nullable: false));
            AddColumn("dbo.Trips", "AllowLuggage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "AllowLuggage");
            DropColumn("dbo.Trips", "AllowSmoking");
            DropColumn("dbo.Trips", "AllowPets");
        }
    }
}
