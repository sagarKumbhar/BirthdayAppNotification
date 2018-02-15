namespace Birthday.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClasses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoginUser", "DeptId", "dbo.Dept");
            DropIndex("dbo.LoginUser", new[] { "DeptId" });
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 50),
                        ShortName = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DistributionList",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Recepients = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        IsIncludeUsers = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        Email = c.String(),
                        Birthdate = c.DateTime(),
                        DepartmentId = c.Int(nullable: false),
                        EmployeeName = c.String(nullable: false, maxLength: 100),
                        PhotoLocation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            AddColumn("dbo.LoginUser", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.LoginUser", "DepartmentId");
            AddForeignKey("dbo.LoginUser", "DepartmentId", "dbo.Department", "Id", cascadeDelete: true);
            DropColumn("dbo.LoginUser", "DeptId");
            DropTable("dbo.Dept");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Dept",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeptName = c.String(),
                        ShortName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.LoginUser", "DeptId", c => c.Int(nullable: false));
            DropForeignKey("dbo.LoginUser", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.Employee", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.DistributionList", "DepartmentId", "dbo.Department");
            DropIndex("dbo.LoginUser", new[] { "DepartmentId" });
            DropIndex("dbo.Employee", new[] { "DepartmentId" });
            DropIndex("dbo.DistributionList", new[] { "DepartmentId" });
            DropColumn("dbo.LoginUser", "DepartmentId");
            DropTable("dbo.Employee");
            DropTable("dbo.DistributionList");
            DropTable("dbo.Department");
            CreateIndex("dbo.LoginUser", "DeptId");
            AddForeignKey("dbo.LoginUser", "DeptId", "dbo.Dept", "Id", cascadeDelete: true);
        }
    }
}
