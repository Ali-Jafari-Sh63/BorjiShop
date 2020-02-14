using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BorjiShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public bool IsCleared { get; set; }

        public double Price { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}