using Lagarsystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Lagarsystem.Repositories;
using PagedList;


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
            var items = SIDB.SearchForItem(SearchTerm);

            //items

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        // GET: Stock
        public ActionResult Index(string searchBase = "All", string SearchTerm = null, int page = 1)
        {
            var items = SIDB.GetAllItems().ToPagedList(page, 10);

            if(Request.IsAjaxRequest())
            {
                items = SIDB.SearchForItem(searchBase, SearchTerm).ToPagedList(page, 10);
                return PartialView("ItemsList", items);
            }

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId, Name, Price, Shelf, Description")] StockItem SI)
        {
            if(SI == null)
               return RedirectToAction("Index");

            if(ModelState.IsValid)
            {
                SIDB.AddItemToDB(SI);
            }
            return Json(new { status = "success", message = "Item Created!" });
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

            if (Request.IsAjaxRequest())
            {
                return PartialView("EditModal", item);
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
                return Json(new { message = "Item Edited!" });
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

            if (Request.IsAjaxRequest())
            {
                return PartialView("RemoveModal", item);
            }

            return View(item);
        }

        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveConfirm(int? id) 
        {
            StockItem item = SIDB.GetItem(id);
            SIDB.RemoveItemFromDB(item);
            return Json(new { message = "Item Removed!" });
        }

        [HttpGet]
        public ActionResult Search(string searchBase, string SearchTerm = null, int page = 1)
        {
            var items = SIDB.SearchForItem(searchBase, SearchTerm).ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("ItemsList", items);
            }

            return RedirectToAction("Index");
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