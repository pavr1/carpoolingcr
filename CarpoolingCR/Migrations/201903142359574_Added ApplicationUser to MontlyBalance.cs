namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedApplicationUsertoMontlyBalance : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUsers", "MonthlyBalanceId", "dbo.MonthlyBalances");
            DropIndex("dbo.ApplicationUsers", new[] { "MonthlyBalanceId" });
            AddColumn("dbo.MonthlyBalances", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.MonthlyBalances", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.MonthlyBalances", "ApplicationUser_Id");
            AddForeignKey("dbo.MonthlyBalances", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
            DropColumn("dbo.ApplicationUsers", "MonthlyBalanceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "MonthlyBalanceId", c => c.Int());
            DropForeignKey("dbo.MonthlyBalances", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.MonthlyBalances", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.MonthlyBalances", "ApplicationUser_Id");
            DropColumn("dbo.MonthlyBalances", "ApplicationUserId");
            CreateIndex("dbo.ApplicationUsers", "MonthlyBalanceId");
            AddForeignKey("dbo.ApplicationUsers", "MonthlyBalanceId", "dbo.MonthlyBalances", "MonthlyBalanceId");
        }
    }
}
