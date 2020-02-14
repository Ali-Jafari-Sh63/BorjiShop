using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BorjiShop.BLL;
using BorjiShop.Models;

namespace BorjiShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private BLL_ShoppingCart db=new BLL_ShoppingCart();
        List<Brand> brandList = new BLL_Brand().GetAllBrand();
        List<Device> deviceList = new BLL_Device().GetAllDevice();
        List<Piecetype> pieceTypeList = new BLL_PieceType().GetAllPieceType();
        List<Piece> pieceList = new BLL_Piece().GetAllPiece();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<ShowShoppingCart> shopCart = new List<ShowShoppingCart>();
            if (Session["ShoppingCart"] != null)
            {
                List<Piece> shop = Session["ShoppingCart"] as List<Piece>;
                foreach (var item in shop)
                {
                    var piece = pieceList.Find(p => p.ID == item.ID);
                    shopCart.Add(new ShowShoppingCart()
                    {
                        PieceId = piece.ID,
                        TypeName = pieceTypeList.Find(t => t.ID == piece.PieceTypeId).PersianName,
                        BrandName = brandList.Find(b => b.ID == piece.BrandId).PersianName,
                        DeviceName = deviceList.Find(d => d.ID == piece.DeviceId).PersianName,
                        Price = piece.Price,
                        FileName = piece.FileName
                    });
                }
            }

            User user = (User) Session["User"];
            User currentUser = new BLL_User().GetUser(user.ID);
            ViewBag.CurrentUser = currentUser;
            return View(shopCart);
        }

        public ActionResult Remove(int id)
        {
            List<Piece> shop = Session["ShoppingCart"] as List<Piece>;
            int index = shop.FindIndex(s => s.ID == id);
            shop.Remove(shop[index]);
            Session["ShoppingCart"] = shop;
            return RedirectToAction("Index");
        }

        public ActionResult PayOff(double price)
        {
            if (Session["User"]== null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (Session["ShoppingCart"]== null)
            {
                return RedirectToAction("Index", "Home");
            }

            User user = (User) Session["User"];
            var order = new Order()
            {
                Date = DateTime.Now,
                Price = price,
                IsCleared = false,
                UserId = user.ID
            };
            var orderId = db.AddOrder(order);

            List<Piece> shop = Session["ShoppingCart"] as List<Piece>;
            foreach (var piece in shop)
            {
                var detail = new OrderDetail()
                {
                    PieceId = piece.ID,
                    OrderId = orderId
                };
                var detaiId = db.AddOrderDetail(detail);
            }


            return null;
        }
    }
}