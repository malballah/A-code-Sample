namespace Truck.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orgfilepath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachments", "OrgFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attachments", "OrgFilePath");
        }
    }
}
