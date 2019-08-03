namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBanktotheApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "BankId", c => c.Int());
            CreateIndex("dbo.ApplicationUsers", "BankId");
            AddForeignKey("dbo.ApplicationUsers", "BankId", "dbo.Banks", "BankId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUsers", "BankId", "dbo.Banks");
            DropIndex("dbo.ApplicationUsers", new[] { "BankId" });
            DropColumn("dbo.ApplicationUsers", "BankId");
        }
    }
}
