using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lagarsystem.Models;
using Lagarsystem.DataAccessLayer;
using System.Data.Entity;
using System.Web.Mvc;
using System.Data.Entity.Core;
using System.Reflection;
using System.ComponentModel;

namespace Lagarsystem.Repositories
{
    public class StoreRepository
    {
        StoreContext SIDB = new StoreContext();

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

        private string propertyValue(StockItem a, string name)
        {
            return typeof(StockItem).GetProperty(name).GetValue(a).ToString();
        }

        public List<StockItem> SearchForItem(string searchBase, string SearchTerm = null)
        {
            List<StockItem> allitems;

            if(searchBase == "Name")
                allitems = SIDB.Items.Where(i => i.Name.ToLower().StartsWith(SearchTerm.ToLower()) || String.IsNullOrEmpty(SearchTerm)).ToList();
            else if(searchBase == "Price")
                allitems = SIDB.Items.Where(i => i.Price.ToString().ToLower().StartsWith(SearchTerm.ToLower()) || String.IsNullOrEmpty(SearchTerm)).ToList();
            else if (searchBase == "Shelf")
                allitems = SIDB.Items.Where(i => i.Shelf.ToLower().StartsWith(SearchTerm.ToLower()) || String.IsNullOrEmpty(SearchTerm)).ToList();
            else if (searchBase == "Description")
                allitems = SIDB.Items.Where(i => i.Description.ToLower().Contains(SearchTerm.ToLower()) || String.IsNullOrEmpty(SearchTerm)).ToList();
            else
                allitems = SIDB.Items.Where(i => String.IsNullOrEmpty(SearchTerm) || i.ItemID.ToString().StartsWith(SearchTerm) || i.Name.ToLower().StartsWith(SearchTerm.ToLower()) || i.Price.ToString().StartsWith(SearchTerm) || i.Shelf.ToLower().StartsWith(SearchTerm.ToLower()) || i.Description.ToLower().StartsWith(SearchTerm.ToLower())).ToList();

            return allitems;
        }

        public List<Autocomplete> GetItems(string SearchTerm = null)
        {
            List<Autocomplete> items = new List<Autocomplete>();
            try
            {
                var results = SIDB.Items.Where(i => SearchTerm == null || i.ItemID.ToString().StartsWith(SearchTerm) || i.Name.ToLower().StartsWith(SearchTerm.ToLower()) || i.Price.ToString().StartsWith(SearchTerm) || i.Shelf.ToLower().StartsWith(SearchTerm.ToLower()) || i.Description.ToLower().StartsWith(SearchTerm.ToLower())).ToList();

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