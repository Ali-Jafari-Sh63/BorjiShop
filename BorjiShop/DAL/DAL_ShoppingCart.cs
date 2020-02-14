using System;
using System.Collections.Generic;
using System.Linq;
using BorjiShop.Models;

namespace BorjiShop.DAL
{
    public class DAL_ShoppingCart
    {
        private BorjiShopContext db = new BorjiShopContext();

        public int AddOrder(Order order)
        {
            int id;
            try
            {
                db.Orders.Add(order);
                db.SaveChanges();
                id = order.Id;
            }
            catch (Exception ex)
            {
                id = 0;
            }

            return id;
        }

        public int AddOrderDetail(OrderDetail detail)
        {
            int id;
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                id = detail.Id;
            }
            catch (Exception ex)
            {
                id = 0;
            }

            return id;
        }
    }
}