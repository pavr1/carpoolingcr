namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedBanks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankId = c.Int(nullable: false, identity: true),
                        BankName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BankId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Banks");
        }
    }
}
