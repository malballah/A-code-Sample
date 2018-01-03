namespace Truck.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wordernumberstring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "WONumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "WONumber", c => c.Int(nullable: false));
        }
    }
}
