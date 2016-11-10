using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lagarsystem.Models;
using Lagarsystem.DataAccessLayer;
using System.Data.Entity;
using System.Web.Mvc;
using System.Data.Entity.Core;

namespace Lagarsystem.Repositories
{
    public class StoreRepository
    {
        StoreContext SIDB = new StoreContext();

        public StoreRepository()
        {
        }

        public List<StockItem> GetAllItems()
        {
            return SIDB.Items.ToList();
        }

        public StockItem GetItem(int? id)
        {
            return SIDB.Items.FirstOrDefault(i => i.ItemID == id);
        }

        public void AddItemToDB(StockItem SI)
        {
            SIDB.Items.Add(SI);
            SIDB.SaveChanges();
        }

        public void EditItem(StockItem item)
        {
            //SIDB.Items.Find(item);

            SIDB.Entry(item).State = EntityState.Modified;

            SIDB.SaveChanges();
        }

        public bool RemoveItemFromDB(StockItem item)
        {
            if (item != null)
            {
                SIDB.Items.Remove(item);
                SIDB.SaveChanges();

                return true;
            }

            return false;
        }

        public List<StockItem> SearchForItem(string SearchTerm = null)
        {
            List<StockItem> allitems = this.SIDB.Items.Where(i => SearchTerm == null || i.ItemID.ToString().StartsWith(SearchTerm) || i.Name.ToLower().StartsWith(SearchTerm.ToLower()) || i.Price.ToString().StartsWith(SearchTerm) || i.Shelf.ToLower().StartsWith(SearchTerm.ToLower()) || i.Description.ToLower().StartsWith(SearchTerm.ToLower())).ToList();
            //.Select(o => new { ItemID = o.ItemID, Name = o.Name, Price = o.Price, Shelf = o.Shelf, Description = o.Description });

            return allitems;
        }

        public List<Autocomplete> GetItems(string SearchTerm = null)
        {
            List<Autocomplete> items = new List<Autocomplete>();
            try
            {
                var results = this.SIDB.Items.Where(i => SearchTerm == null || i.ItemID.ToString().StartsWith(SearchTerm) || i.Name.ToLower().StartsWith(SearchTerm.ToLower()) || i.Price.ToString().StartsWith(SearchTerm) || i.Shelf.ToLower().StartsWith(SearchTerm.ToLower()) || i.Description.ToLower().StartsWith(SearchTerm.ToLower())).ToList();

                foreach (var r in results)
                {
                    // create objects
                    Autocomplete item = new Autocomplete()
                    {
                        Name = r.Name,
                        ItemID = r.ItemID,
                        Price = r.Price,
                        Shelf = r.Shelf,
                        Description = r.Description,
                    };

                    items.Add(item);
                }

            }
            catch (EntityCommandExecutionException eceex)
            {
                if (eceex.InnerException != null)
                {
                    throw eceex.InnerException;
                }
                throw;
            }
            catch
            {
                throw;
            }
            return items;
        }

    }
}