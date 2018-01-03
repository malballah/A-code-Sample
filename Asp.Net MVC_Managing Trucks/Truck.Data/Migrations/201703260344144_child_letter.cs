namespace Truck.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class child_letter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "ChildLetter", c => c.String());
            AddColumn("dbo.Invoices", "HoldReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "HoldReason");
            DropColumn("dbo.Assignments", "ChildLetter");
        }
    }
}
