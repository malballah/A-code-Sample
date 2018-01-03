namespace Timesheet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Employee_Code = c.String(nullable: false, maxLength: 9),
                        Project_Tracker_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.Employee_Code, cascadeDelete: true)
                .ForeignKey("dbo.Project_Tracker", t => t.Project_Tracker_Id, cascadeDelete: true)
                .Index(t => t.Employee_Code)
                .Index(t => t.Project_Tracker_Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 9),
                        Name = c.String(nullable: false, maxLength: 255),
                        User_Id = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Employee_Current_Project",
                c => new
                    {
                        Employee_Code = c.String(nullable: false, maxLength: 9),
                        Project_Code = c.String(nullable: false, maxLength: 4),
                    })
                .PrimaryKey(t => new { t.Employee_Code, t.Project_Code })
                .ForeignKey("dbo.Project", t => t.Project_Code, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.Employee_Code, cascadeDelete: true)
                .Index(t => t.Employee_Code)
                .Index(t => t.Project_Code);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 4),
                        Description = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Project_Tracker",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Project_Code = c.String(nullable: false, maxLength: 4),
                        Tracker_Code = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tracker", t => t.Tracker_Code, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.Project_Code, cascadeDelete: true)
                .Index(t => t.Project_Code)
                .Index(t => t.Tracker_Code);
            
            CreateTable(
                "dbo.Tracker",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Time_Card",
                c => new
                    {
                        Assignment_Id = c.Int(nullable: false),
                        Work_Date_Id = c.DateTime(nullable: false),
                        Hours_Worked = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.Assignment_Id, t.Work_Date_Id })
                .ForeignKey("dbo.Assignment", t => t.Assignment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Work_Date", t => t.Work_Date_Id, cascadeDelete: true)
                .Index(t => t.Assignment_Id)
                .Index(t => t.Work_Date_Id);
            
            CreateTable(
                "dbo.Work_Date",
                c => new
                    {
                        Id_Date = c.DateTime(nullable: false),
                        YYYYMM_Code = c.Int(nullable: false),
                        Day_of_Month = c.Int(nullable: false),
                        Is_Non_Working_Day = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Date)
                .ForeignKey("dbo.YYYYMM", t => t.YYYYMM_Code, cascadeDelete: true)
                .Index(t => t.YYYYMM_Code);
            
            CreateTable(
                "dbo.YYYYMM",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Days_in_Month = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.YYYYMM_Locked",
                c => new
                    {
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Work_Date", "YYYYMM_Code", "dbo.YYYYMM");
            DropForeignKey("dbo.Time_Card", "Work_Date_Id", "dbo.Work_Date");
            DropForeignKey("dbo.Time_Card", "Assignment_Id", "dbo.Assignment");
            DropForeignKey("dbo.Employee_Current_Project", "Employee_Code", "dbo.Employee");
            DropForeignKey("dbo.Project_Tracker", "Project_Code", "dbo.Project");
            DropForeignKey("dbo.Project_Tracker", "Tracker_Code", "dbo.Tracker");
            DropForeignKey("dbo.Assignment", "Project_Tracker_Id", "dbo.Project_Tracker");
            DropForeignKey("dbo.Employee_Current_Project", "Project_Code", "dbo.Project");
            DropForeignKey("dbo.Assignment", "Employee_Code", "dbo.Employee");
            DropIndex("dbo.Work_Date", new[] { "YYYYMM_Code" });
            DropIndex("dbo.Time_Card", new[] { "Work_Date_Id" });
            DropIndex("dbo.Time_Card", new[] { "Assignment_Id" });
            DropIndex("dbo.Project_Tracker", new[] { "Tracker_Code" });
            DropIndex("dbo.Project_Tracker", new[] { "Project_Code" });
            DropIndex("dbo.Employee_Current_Project", new[] { "Project_Code" });
            DropIndex("dbo.Employee_Current_Project", new[] { "Employee_Code" });
            DropIndex("dbo.Assignment", new[] { "Project_Tracker_Id" });
            DropIndex("dbo.Assignment", new[] { "Employee_Code" });
            DropTable("dbo.YYYYMM_Locked");
            DropTable("dbo.YYYYMM");
            DropTable("dbo.Work_Date");
            DropTable("dbo.Time_Card");
            DropTable("dbo.Tracker");
            DropTable("dbo.Project_Tracker");
            DropTable("dbo.Project");
            DropTable("dbo.Employee_Current_Project");
            DropTable("dbo.Employee");
            DropTable("dbo.Assignment");
        }
    }
}
