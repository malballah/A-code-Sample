namespace Truck.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjustment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adjustments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OriginInvoiceId = c.Int(nullable: false),
                        OriginAdjustmentId = c.Int(nullable: false),
                        AppliedAssignmentId = c.Int(nullable: true),
                        CustomerId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.AppliedAssignmentId)
                .ForeignKey("dbo.Invoices", t => t.OriginInvoiceId)
                .ForeignKey("dbo.AdjustmentStatus", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.OriginInvoiceId)
                .Index(t => t.AppliedAssignmentId)
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adjustments", "StatusId", "dbo.AdjustmentStatus");
            DropForeignKey("dbo.Adjustments", "OriginInvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Adjustments", "AppliedAssignmentId", "dbo.Assignments");
            DropIndex("dbo.Adjustments", new[] { "StatusId" });
            DropIndex("dbo.Adjustments", new[] { "AppliedAssignmentId" });
            DropIndex("dbo.Adjustments", new[] { "OriginInvoiceId" });
            DropTable("dbo.Adjustments");
        }
    }
}
