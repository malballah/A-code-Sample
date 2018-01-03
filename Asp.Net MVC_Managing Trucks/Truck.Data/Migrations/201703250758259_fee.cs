namespace Truck.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "Fee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assignments", "Fee");
        }
    }
}
