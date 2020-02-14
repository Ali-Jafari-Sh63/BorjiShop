using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BorjiShop.BLL;
using BorjiShop.Models;

namespace BorjiShop.Controllers
{
    public class UsController : Controller
    {
        private BLL_Us db = new BLL_Us();
        // GET: Us
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            if (Session["User"] != null)
            {
                var user = Session["User"] as User;
                ViewBag.Email = user.Email;
                ViewBag.Name = user.FirstName + " " + user.LastName;
            }
            return View();
        }

        public ActionResult AddMessage(Message message)
        {
            string result;
            if (message.Email == "" || message.Text == "")
            {
                result = "درج پست الکترونیک و متن پیام الزامی است.";
                return Json(result);
            }

            message.Read = false;
            message.Answered = false;
            message.Date = DateTime.Now;
            result = db.AddMessage(message);
            return Json(result);
        }
    }
}