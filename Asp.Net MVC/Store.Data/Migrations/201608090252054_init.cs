namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CPU",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CPUSocketId = c.Int(nullable: false),
                        PowerConsumption = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CPUSocket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Memory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Size = c.Int(nullable: false),
                        PowerConsumption = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Motherboard",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CPUSocketId = c.Int(nullable: false),
                        MemorySlots = c.Int(nullable: false),
                        PowerConsumption = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PCMemory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemoryId = c.Int(nullable: false),
                        PCId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Memory", t => t.MemoryId, cascadeDelete: true)
                .ForeignKey("dbo.PC", t => t.PCId, cascadeDelete: true)
                .Index(t => t.MemoryId)
                .Index(t => t.PCId);
            
            CreateTable(
                "dbo.PC",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 200),
                        CPUId = c.Int(nullable: false),
                        MotherboardId = c.Int(nullable: false),
                        PowerSupplyId = c.Int(nullable: false),
                        AssemblyNeeded = c.Boolean(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CPU", t => t.CPUId, cascadeDelete: true)
                .ForeignKey("dbo.Motherboard", t => t.MotherboardId, cascadeDelete: true)
                .ForeignKey("dbo.PowerSupply", t => t.PowerSupplyId, cascadeDelete: true)
                .Index(t => t.CPUId)
                .Index(t => t.MotherboardId)
                .Index(t => t.PowerSupplyId);
            
            CreateTable(
                "dbo.PowerSupply",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        MaxPowerOutput = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PC", "PowerSupplyId", "dbo.PowerSupply");
            DropForeignKey("dbo.PCMemory", "PCId", "dbo.PC");
            DropForeignKey("dbo.PC", "MotherboardId", "dbo.Motherboard");
            DropForeignKey("dbo.PC", "CPUId", "dbo.CPU");
            DropForeignKey("dbo.PCMemory", "MemoryId", "dbo.Memory");
            DropIndex("dbo.PC", new[] { "PowerSupplyId" });
            DropIndex("dbo.PC", new[] { "MotherboardId" });
            DropIndex("dbo.PC", new[] { "CPUId" });
            DropIndex("dbo.PCMemory", new[] { "PCId" });
            DropIndex("dbo.PCMemory", new[] { "MemoryId" });
            DropTable("dbo.PowerSupply");
            DropTable("dbo.PC");
            DropTable("dbo.PCMemory");
            DropTable("dbo.Motherboard");
            DropTable("dbo.Memory");
            DropTable("dbo.CPUSocket");
            DropTable("dbo.CPU");
        }
    }
}
