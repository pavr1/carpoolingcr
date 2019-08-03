namespace CarpoolingCR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedProfileQualificationandQualificationsmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileQualifications",
                c => new
                    {
                        ProfileQualificationId = c.Int(nullable: false, identity: true),
                        Userid = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProfileQualificationId)
                .ForeignKey("dbo.ApplicationUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        QualificationId = c.Int(nullable: false, identity: true),
                        FromUserId = c.Int(nullable: false),
                        ToUserId = c.Int(nullable: false),
                        Starts = c.Int(nullable: false),
                        Comments = c.String(),
                        FromUser_Id = c.String(maxLength: 128),
                        ToUser_Id = c.String(maxLength: 128),
                        ProfileQualification_ProfileQualificationId = c.Int(),
                    })
                .PrimaryKey(t => t.QualificationId)
                .ForeignKey("dbo.ApplicationUsers", t => t.FromUser_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ToUser_Id)
                .ForeignKey("dbo.ProfileQualifications", t => t.ProfileQualification_ProfileQualificationId)
                .Index(t => t.FromUser_Id)
                .Index(t => t.ToUser_Id)
                .Index(t => t.ProfileQualification_ProfileQualificationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileQualifications", "User_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Qualifications", "ProfileQualification_ProfileQualificationId", "dbo.ProfileQualifications");
            DropForeignKey("dbo.Qualifications", "ToUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Qualifications", "FromUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Qualifications", new[] { "ProfileQualification_ProfileQualificationId" });
            DropIndex("dbo.Qualifications", new[] { "ToUser_Id" });
            DropIndex("dbo.Qualifications", new[] { "FromUser_Id" });
            DropIndex("dbo.ProfileQualifications", new[] { "User_Id" });
            DropTable("dbo.Qualifications");
            DropTable("dbo.ProfileQualifications");
        }
    }
}
