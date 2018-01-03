namespace Truck.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assignment_return : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "ReturnReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assignments", "ReturnReason");
        }
    }
}
