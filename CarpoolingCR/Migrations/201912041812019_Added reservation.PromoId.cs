namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedreservationPromoId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "PromoId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "PromoId");
        }
    }
}
