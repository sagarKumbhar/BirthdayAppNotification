namespace Birthday.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserCode = c.String(nullable: false, maxLength: 10),
                        Password = c.String(nullable: false, maxLength: 10),
                        DepartmentId = c.Int(nullable: false),
                        UserProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        ShortName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        ProfileName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginUser", "UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.LoginUser", "DepartmentId", "dbo.Department");
            DropIndex("dbo.LoginUser", new[] { "UserProfileId" });
            DropIndex("dbo.LoginUser", new[] { "DepartmentId" });
            DropTable("dbo.UserProfile");
            DropTable("dbo.Department");
            DropTable("dbo.LoginUser");
        }
    }
}
