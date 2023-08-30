namespace ResumeManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Salary = c.Int(nullable: false),
                        Picture = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.QualificationEntries",
                c => new
                    {
                        QualificationEntryId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        QualificationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QualificationEntryId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Qualifications", t => t.QualificationId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.QualificationId);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        QualificationId = c.Int(nullable: false, identity: true),
                        QualificationName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.QualificationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QualificationEntries", "QualificationId", "dbo.Qualifications");
            DropForeignKey("dbo.QualificationEntries", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.QualificationEntries", new[] { "QualificationId" });
            DropIndex("dbo.QualificationEntries", new[] { "EmployeeId" });
            DropTable("dbo.Qualifications");
            DropTable("dbo.QualificationEntries");
            DropTable("dbo.Employees");
        }
    }
}
