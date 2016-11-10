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

        public ActionResult AutoComplete(string SearchTerm = null)
        {
            List<StockItem> items = SIDB.SearchForItem(SearchTerm);
            
            //items

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        // GET: Stock
        public ActionResult Index(string SearchTerm = null)
        {
            List<StockItem> items = SIDB.SearchForItem(SearchTerm);

            if(Request.IsAjaxRequest())
            {
                return PartialView("ItemsList", items);
            }

            return View(items);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId, Name, Price, Shelf, Description")] StockItem SI)
        {
            if(ModelState.IsValid)
            {
                SIDB.AddItemToDB(SI);
            }
            return Json(new { message = "Success" }, JsonRequestBehavior.AllowGet); 
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
        public ActionResult Edit([Bind(Include = "ItemId, Name, Price, Shelf, Description")] StockItem item)
        {
            if (ModelState.IsValid)
            {
                SIDB.EditItem(item);
                return RedirectToAction("Index");
            }
            return View(item);
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
            SIDB.RemoveItemFromDB(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(string SearchTerm = null)
        {
            List<StockItem> items = SIDB.SearchForItem(SearchTerm);

            if (Request.IsAjaxRequest())
            {
                return PartialView("ItemsList", items);
            }

            return View(items);
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