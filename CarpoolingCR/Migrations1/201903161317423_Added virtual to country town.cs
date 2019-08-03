namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedvirtualtocountrytown : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Towns", "CountryId", c => c.Int());
            CreateIndex("dbo.Towns", "CountryId");
            AddForeignKey("dbo.Towns", "CountryId", "dbo.Countries", "CountryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Towns", "CountryId", "dbo.Countries");
            DropIndex("dbo.Towns", new[] { "CountryId" });
            DropColumn("dbo.Towns", "CountryId");
        }
    }
}
