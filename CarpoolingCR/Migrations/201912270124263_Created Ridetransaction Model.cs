namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedRidetransactionModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RideTransactions",
                c => new
                    {
                        RideTransactionId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        RequestedDate = c.DateTime(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        TransactionStatus = c.Int(nullable: false),
                        AppliedDate = c.DateTime(),
                        ReferencedNumber = c.String(),
                        Image = c.Binary(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.RideTransactionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RideTransactions");
        }
    }
}
