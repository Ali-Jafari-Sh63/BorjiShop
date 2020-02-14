using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjiShop.Models
{
    public class ShowShoppingCart
    {
        [Key]
        public int PieceId { get; set; }

        public string TypeName { get; set; }

        public string DeviceName { get; set; }

        public string BrandName { get; set; }

        public decimal Price { get; set; }

        public string FileName { get; set; }

    }
}