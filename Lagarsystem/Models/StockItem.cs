using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lagarsystem.Models
{
    public class StockItem
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        public string Name { get; set; }

        //[DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Shelf { get; set; }
        [Required]
        public string Description { get; set; }

    }
}