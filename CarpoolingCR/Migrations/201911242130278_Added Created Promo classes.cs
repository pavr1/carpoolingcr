namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreatedPromoclasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BalanceHistorials",
                c => new
                    {
                        BalanceHistorialId = c.Int(nullable: false, identity: true),
                        UserPromoId = c.Int(),
                        TripId = c.Int(),
                        Detail = c.String(),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BalanceHistorialId);
            
            CreateTable(
                "dbo.Promoes",
                c => new
                    {
                        PromoId = c.Int(nullable: false, identity: true),
                        PromoTypeId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        UntilAssignedAmountRunsOut = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        MaxAmountPerUser = c.Int(nullable: false),
                        Description = c.String(),
                        MaxAmountToSpend = c.Decimal(precision: 18, scale: 2),
                        AmountAvailable = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PromoId)
                .ForeignKey("dbo.PromoTypes", t => t.PromoTypeId, cascadeDelete: true)
                .Index(t => t.PromoTypeId);
            
            CreateTable(
                "dbo.PromoTypes",
                c => new
                    {
                        PromoTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PromoTypeId);
            
            CreateTable(
                "dbo.UserPromos",
                c => new
                    {
                        UserPromosId = c.Int(nullable: false, identity: true),
                        PromoId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserPromosId)
                .ForeignKey("dbo.Promoes", t => t.PromoId, cascadeDelete: true)
                .Index(t => t.PromoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPromos", "PromoId", "dbo.Promoes");
            DropForeignKey("dbo.Promoes", "PromoTypeId", "dbo.PromoTypes");
            DropIndex("dbo.UserPromos", new[] { "PromoId" });
            DropIndex("dbo.Promoes", new[] { "PromoTypeId" });
            DropTable("dbo.UserPromos");
            DropTable("dbo.PromoTypes");
            DropTable("dbo.Promoes");
            DropTable("dbo.BalanceHistorials");
        }
    }
}
