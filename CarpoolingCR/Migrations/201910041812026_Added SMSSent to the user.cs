namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSMSSenttotheuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "SMSSent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "SMSSent");
        }
    }
}
