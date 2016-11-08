using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lagarsystem.Models;
using Lagarsystem.DataAccessLayer;
using System.Data.Entity;
using System.Web.Mvc;

namespace Lagarsystem.Repositories
{
    public class StoreRepository
    {
        StoreContext SIDB = new StoreContext();
        List<StockItem> itemList;

        public StoreRepository()
        {
            itemList = SIDB.Items.ToList();
        }

        public List<StockItem> GetAllItems(){
            return this.itemList;
        }

        public StockItem GetItem(int? id)
        {
            return this.itemList.FirstOrDefault(i => i.ItemID == id);
        }

        public void AddItemToDB(StockItem SI)
        {
            SIDB.Items.Add(SI);
            SIDB.SaveChanges();
            this.itemList.Add(SI);
        }

        public void EditItem(StockItem item, EntityState state)
        {
            SIDB.Entry(item).State = state;
            SIDB.SaveChanges();
        }

        public bool RemoveItemFromDB(int? id)
        {
            StockItem item = this.itemList.Find(i => i.ItemID == id);
            if (item != null)
            {
                SIDB.Items.Remove(item);
                SIDB.SaveChanges();
                this.itemList.Remove(item);

                return true;
            }

            return false;
        }

        public List<StockItem> SearchForItem(string SearchTerm)
        {
            List<StockItem> allitemss = this.itemList;

            if (!String.IsNullOrEmpty(SearchTerm))
                allitemss = allitemss.Where(i => i.ItemID.ToString().Contains(SearchTerm) || i.Name.ToLower().Contains(SearchTerm.ToLower()) || i.Price.ToString().Contains(SearchTerm) || i.Shelf.ToLower().Contains(SearchTerm.ToLower()) || i.Description.ToLower().Contains(SearchTerm.ToLower())).ToList();

            return allitemss;
        }
    }
}