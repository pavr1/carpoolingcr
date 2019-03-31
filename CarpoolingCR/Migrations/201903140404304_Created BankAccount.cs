namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedBankAccount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUsers", "BankId", "dbo.Banks");
            DropIndex("dbo.ApplicationUsers", new[] { "BankId" });
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankAccountId = c.Int(nullable: false, identity: true),
                        BankId = c.Int(nullable: false),
                        SavingsAccount = c.String(nullable: false),
                        Sinpe = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BankAccountId)
                .ForeignKey("dbo.Banks", t => t.BankId, cascadeDelete: true)
                .Index(t => t.BankId);
            
            AddColumn("dbo.ApplicationUsers", "BankAccountId", c => c.Int());
            CreateIndex("dbo.ApplicationUsers", "BankAccountId");
            AddForeignKey("dbo.ApplicationUsers", "BankAccountId", "dbo.BankAccounts", "BankAccountId");
            DropColumn("dbo.ApplicationUsers", "BankId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "BankId", c => c.Int());
            DropForeignKey("dbo.ApplicationUsers", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.BankAccounts", "BankId", "dbo.Banks");
            DropIndex("dbo.BankAccounts", new[] { "BankId" });
            DropIndex("dbo.ApplicationUsers", new[] { "BankAccountId" });
            DropColumn("dbo.ApplicationUsers", "BankAccountId");
            DropTable("dbo.BankAccounts");
            CreateIndex("dbo.ApplicationUsers", "BankId");
            AddForeignKey("dbo.ApplicationUsers", "BankId", "dbo.Banks", "BankId");
        }
    }
}
