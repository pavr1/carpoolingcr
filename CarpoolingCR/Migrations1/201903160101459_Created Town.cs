namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedTown : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        TownId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TownId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Towns");
        }
    }
}
