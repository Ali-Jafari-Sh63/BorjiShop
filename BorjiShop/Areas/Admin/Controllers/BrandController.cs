using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BorjiShop.Models;
using BorjiShop.BLL;

namespace BorjiShop.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        private BLL_Brand db = new BLL_Brand();
        // GET: Admin/Brand
        public ActionResult Index()
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            return View();
        }

        [HttpPost]
        public ActionResult GetBrands(int page, int editID)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = db.GetBrandList(page);
            var lastPage = db.LastPageNumber();
            object finalResult = new Tuple<List<Brand>, int, int, int>(result, page, lastPage, editID);
            return Json(finalResult);
        }

        public ActionResult AddBrand(Brand brand)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = "";
            if (!db.ExistBrand(brand))
            {
                result = brand == null ? "برند مورد نظر معتبر نمی باشد." : db.AddBrand(brand);
            }
            else
            {
                result = "برند مورد نظر قبلاً ثبت شده است.";
            }

            var lastPage = db.LastPageNumber();
            object finalResult = new Tuple<string, int>(result, lastPage);
            return Json(finalResult);
        }

        public ActionResult UpdateBrand(int ID, string englishName, string persianName, int page)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var brand = new Brand();
            brand.EnglishName = englishName;
            brand.PersianName = persianName;

            var result = "";
            if (db.ExistBrand(ID))
            {
                if (db.ExistBrand(brand))
                {
                    result = "برند مورد نظر قبلاً ثبت شده است.";
                }
                else
                    result = db.UpdateBrand(ID, englishName, persianName);
            }
            else
            {
                result = "برند مورد نظر معتبر نیست.";
            }

            object finalResult = new Tuple<string, int>(result, page);
            return Json(finalResult);
        }

        public ActionResult DeleteBrand(int ID)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = "";
            if (db.ExistBrand(ID))
            {
                result = db.DeleteBrand(ID);
            }
            else
            {
                result = "برند مورد نظر معتبر نیست.";
            }

            var lastPage = db.LastPageNumber();
            object finalResult = new Tuple<string, int>(result, lastPage);
            return Json(finalResult);
        }
    }
}