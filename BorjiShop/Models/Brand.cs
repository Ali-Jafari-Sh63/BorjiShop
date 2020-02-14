using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BorjiShop.Models
{
    public class Brand
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string EnglishName { get; set; }

        [StringLength(50)]
        public string PersianName { get; set; }

        public ICollection<Device> Devices { get; set; }
    }
}