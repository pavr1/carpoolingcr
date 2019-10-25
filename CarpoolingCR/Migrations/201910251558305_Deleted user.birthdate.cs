namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deleteduserbirthdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApplicationUsers", "BirthDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "BirthDate", c => c.DateTime());
        }
    }
}
