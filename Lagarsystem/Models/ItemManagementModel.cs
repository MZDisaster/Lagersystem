using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lagarsystem.Models
{
    public class ItemManagementModel
    {
        public StockItem StockItemObject { get; set; }
        public IEnumerable<StockItem> IEnumerableStockItem { get; set; }

    }
}