namespace Truck.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdjustmentStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Number = c.Int(nullable: false),
                        Fuel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Funded = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPayable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Signature = c.String(),
                        StatusId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        SubmitDate = c.DateTime(),
                        ApproveDate = c.DateTime(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssignmentStatus", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.AssignmentStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
 
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignmentId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        WONumber = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CheckAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerName = c.String(),
                        Date = c.DateTime(),
                        CheckDate = c.DateTime(),
                        CheckNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .ForeignKey("dbo.InvoiceStatus", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.AssignmentId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.InvoiceAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        AttachmentId = c.Int(nullable: false),
                        IsCheck = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attachments", t => t.AttachmentId, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.AttachmentId);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Name = c.String(),
                        GoogleDriveId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "StatusId", "dbo.InvoiceStatus");
            DropForeignKey("dbo.InvoiceAttachments", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.InvoiceAttachments", "AttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.Invoices", "AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.Assignments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Assignments", "StatusId", "dbo.AssignmentStatus");
            DropIndex("dbo.InvoiceAttachments", new[] { "AttachmentId" });
            DropIndex("dbo.InvoiceAttachments", new[] { "InvoiceId" });
            DropIndex("dbo.Invoices", new[] { "StatusId" });
            DropIndex("dbo.Invoices", new[] { "AssignmentId" });
            DropIndex("dbo.Assignments", new[] { "CustomerId" });
            DropIndex("dbo.Assignments", new[] { "StatusId" });
            DropTable("dbo.InvoiceStatus");
            DropTable("dbo.Attachments");
            DropTable("dbo.InvoiceAttachments");
            DropTable("dbo.Invoices");
            DropTable("dbo.AssignmentStatus");
            DropTable("dbo.Assignments");
            DropTable("dbo.AdjustmentStatus");
        }
    }
}
