namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountAddedToPCMemory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PCMemory", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PCMemory", "Count");
        }
    }
}
