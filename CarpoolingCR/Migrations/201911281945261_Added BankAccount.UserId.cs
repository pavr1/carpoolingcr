namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBankAccountUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccounts", "UserId");
        }
    }
}
