namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedMontlhyBalanceandTransactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonthlyBalances",
                c => new
                    {
                        MonthlyBalanceId = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        ApplicationUserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MonthlyBalanceId)
                .ForeignKey("dbo.ApplicationUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.MonthlyTransactions",
                c => new
                    {
                        MonthlyTransactionsId = c.Int(nullable: false, identity: true),
                        MonthlyBalanceId = c.Int(nullable: false),
                        InitialBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditType = c.Int(nullable: false),
                        CreditReference = c.Int(),
                        DebitAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DebitType = c.Int(nullable: false),
                        DebitReference = c.Int(),
                        FinalBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MonthlyTransactionsId)
                .ForeignKey("dbo.MonthlyBalances", t => t.MonthlyBalanceId, cascadeDelete: true)
                .Index(t => t.MonthlyBalanceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonthlyTransactions", "MonthlyBalanceId", "dbo.MonthlyBalances");
            DropForeignKey("dbo.MonthlyBalances", "User_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.MonthlyTransactions", new[] { "MonthlyBalanceId" });
            DropIndex("dbo.MonthlyBalances", new[] { "User_Id" });
            DropTable("dbo.MonthlyTransactions");
            DropTable("dbo.MonthlyBalances");
        }
    }
}
