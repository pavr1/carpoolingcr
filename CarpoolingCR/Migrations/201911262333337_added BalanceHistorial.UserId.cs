namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedBalanceHistorialUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BalanceHistorials", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BalanceHistorials", "UserId");
        }
    }
}
