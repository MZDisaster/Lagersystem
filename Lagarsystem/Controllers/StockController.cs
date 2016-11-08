using Lagarsystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Lagarsystem.Repositories;


namespace Lagarsystem.Controllers
{
    public class StockController : Controller
    {
        public StoreRepository SIDB;

        public StockController()
        {
            SIDB = new StoreRepository();
            //ViewData.Add("Message", (string)null);
        }

        // GET: Stock
        public ActionResult Index()
        {
            return View(SIDB.GetAllItems());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId, Name, Price, Shelf, Description")] StockItem SI)
        {
            if(ModelState.IsValid)
            {
                SIDB.AddItemToDB(SI);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            StockItem item = SIDB.GetItem(id);

            if (item == null)
            {
                TempData["Message"] = "Item not found!";
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId, Name, Price, Shelf, Description")] StockItem SI)
        {
            if (ModelState.IsValid)
            {
                SIDB.EditItem(SI, EntityState.Modified);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            StockItem item = SIDB.GetItem(id);

            if (item == null)
            {
                TempData["Message"] = "Item not found!";
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveConfirm(int? id) 
        {
            StockItem item = SIDB.GetItem(id);
            SIDB.RemoveItemFromDB(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(string SearchTerm)
        {
            IEnumerable<StockItem> allitems = SIDB.SearchForItem(SearchTerm);
            
            return View(allitems);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            StockItem item = SIDB.GetItem(id);

            if(item == null)
            {
                TempData["Message"] = "Item not found!";
                return RedirectToAction("Index");
            }

            return View(item);
        }
    }
}