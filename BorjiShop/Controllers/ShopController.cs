using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BorjiShop.Models;

namespace BorjiShop.Controllers
{
    public class ShopController : Controller
    { 
        // GET: Shop
        public string Index()
        {
            int count = 0;
            var sessions = System.Web.HttpContext.Current.Session;
            List<Piece> shopCart = new List<Piece>();
            if (sessions["ShoppingCart"] != null)
            {
                shopCart = sessions["ShoppingCart"] as List<Piece>;
                count = shopCart.Count();
            }
            return "تعداد کالا: " + count;
        }

        public string Get(int pieceId)
        {
            var sessions = System.Web.HttpContext.Current.Session;
            List<Piece> shopCart = new List<Piece>();
            if (sessions["ShoppingCart"] != null)
            {
                shopCart = sessions["ShoppingCart"] as List<Piece>;
                if (shopCart.Any(s=>s.ID == pieceId))
                {
                    //int index = shopCart.FindIndex(s => s.ID == pieceId);
                    return "این قطعه در سبد خرید شما وجود دارد.";
                }
                else
                {
                    shopCart.Add(new Piece()
                    {
                        ID = pieceId
                    });
                }
            }
            else
            {
                shopCart.Add(new Piece()
                {
                    ID = pieceId
                });   
            }

            Session["ShoppingCart"] = shopCart;
            return Index();
        }
    }
}