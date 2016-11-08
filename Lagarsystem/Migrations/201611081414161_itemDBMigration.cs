namespace Lagarsystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemDBMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockItems",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Shelf = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StockItems");
        }
    }
}
