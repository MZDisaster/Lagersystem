namespace Lagarsystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StockItems", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.StockItems", "Shelf", c => c.String(nullable: false));
            AlterColumn("dbo.StockItems", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StockItems", "Description", c => c.String());
            AlterColumn("dbo.StockItems", "Shelf", c => c.String());
            AlterColumn("dbo.StockItems", "Name", c => c.String());
        }
    }
}
