namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserPromoAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "PromoAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "PromoAmount");
        }
    }
}
