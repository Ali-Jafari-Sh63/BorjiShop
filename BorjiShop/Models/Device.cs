using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjiShop.Models
{
    public class Device
    {
        [Key]
        public int ID { get; set; }
        public string EnglishName { get; set; }
        public string PersianName { get; set; }
        public int BrandId { get; set; }
    }
}   