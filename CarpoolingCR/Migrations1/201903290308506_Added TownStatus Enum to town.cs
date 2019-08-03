namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTownStatusEnumtotown : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Towns", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Towns", "Status");
        }
    }
}
