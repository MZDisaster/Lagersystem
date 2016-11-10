using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Lagarsystem.Models
{
    public class Autocomplete
    {
        public int ItemID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Shelf { get; set; }
        public string Description { get; set; }
    }

}