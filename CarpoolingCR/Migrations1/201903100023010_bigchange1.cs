namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bigchange1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUsers", "CountryId", "dbo.Countries");
            DropIndex("dbo.ApplicationUsers", new[] { "CountryId" });
            AlterColumn("dbo.ApplicationUsers", "CountryId", c => c.Int());
            CreateIndex("dbo.ApplicationUsers", "CountryId");
            AddForeignKey("dbo.ApplicationUsers", "CountryId", "dbo.Countries", "CountryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUsers", "CountryId", "dbo.Countries");
            DropIndex("dbo.ApplicationUsers", new[] { "CountryId" });
            AlterColumn("dbo.ApplicationUsers", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.ApplicationUsers", "CountryId");
            AddForeignKey("dbo.ApplicationUsers", "CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
        }
    }
}
