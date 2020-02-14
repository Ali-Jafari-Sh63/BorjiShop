using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BorjiShop.Models;

namespace BorjiShop.Areas.Admin.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Admin/Management
        public ActionResult Index()
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login","User");
            return View();
        }
    }
}