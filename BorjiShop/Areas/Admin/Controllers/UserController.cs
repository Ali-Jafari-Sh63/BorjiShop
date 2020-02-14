using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BorjiShop.Models;
using BorjiShop.BLL;

namespace BorjiShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private BLL_User db = new BLL_User();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
            {
                return RedirectToAction("Login");
            }

            string password = PublicFunction.HashPassword(user.Password);
            user.Password = PublicFunction.HashPassword(password);
            string loginStatus = db.Login(user);
            if (loginStatus == "Success" && db.IsAdmin(user))
            {
                Session["AdminUser"] = user;
                return RedirectToAction("Index", "Management");
            }
            return RedirectToAction("Login");
        }

     
    }
}