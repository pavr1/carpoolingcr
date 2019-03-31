namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMonthlyBalancetoApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MonthlyBalances", "User_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.MonthlyBalances", new[] { "User_Id" });
            AddColumn("dbo.ApplicationUsers", "MonthlyBalanceId", c => c.Int());
            CreateIndex("dbo.ApplicationUsers", "MonthlyBalanceId");
            AddForeignKey("dbo.ApplicationUsers", "MonthlyBalanceId", "dbo.MonthlyBalances", "MonthlyBalanceId");
            DropColumn("dbo.MonthlyBalances", "ApplicationUserId");
            DropColumn("dbo.MonthlyBalances", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MonthlyBalances", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.MonthlyBalances", "ApplicationUserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ApplicationUsers", "MonthlyBalanceId", "dbo.MonthlyBalances");
            DropIndex("dbo.ApplicationUsers", new[] { "MonthlyBalanceId" });
            DropColumn("dbo.ApplicationUsers", "MonthlyBalanceId");
            CreateIndex("dbo.MonthlyBalances", "User_Id");
            AddForeignKey("dbo.MonthlyBalances", "User_Id", "dbo.ApplicationUsers", "Id");
        }
    }
}
