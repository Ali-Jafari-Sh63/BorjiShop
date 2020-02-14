using System.Collections.Generic;
using BorjiShop.DAL;
using BorjiShop.Models;

namespace BorjiShop.BLL
{
    public class BLL_ShoppingCart
    {
        private DAL_ShoppingCart dal_ShoppingCart = new DAL_ShoppingCart();

        public int AddOrder(Order order)
        {
            return dal_ShoppingCart.AddOrder(order);
        }

        public int AddOrderDetail(OrderDetail detail)
        {
            return dal_ShoppingCart.AddOrderDetail(detail);
        }
    }
}