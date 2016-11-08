namespace Lagarsystem.Migrations
{
    using Lagarsystem.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Lagarsystem.DataAccessLayer.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Lagarsystem.DataAccessLayer.StoreContext context)
        {
            context.Items.AddOrUpdate(i => i.ItemID,
                new StockItem() { ItemID = 1, Name = "Pepsi", Price = 15, Shelf = "KassaShelf", Description = "Pepsi" },
                new StockItem() { ItemID = 2, Name = "CocaCola", Price = 15, Shelf = "KassaShelf", Description = "CocaCola" },
                new StockItem() { ItemID = 3, Name = "Heniken", Price = 20, Shelf = "KassaShelf", Description = "Alkohol" },
                new StockItem() { ItemID = 4, Name = "Mars", Price = 12, Shelf = "KassaShelf", Description = "Some Sweet sweet chocolate" },
                new StockItem() { ItemID = 5, Name = "Lays", Price = 17, Shelf = "BackShelf", Description = "Chips" },
                new StockItem() { ItemID = 6, Name = "Skettles", Price = 15, Shelf = "KassaShelf", Description = "Sour sweets" }
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
