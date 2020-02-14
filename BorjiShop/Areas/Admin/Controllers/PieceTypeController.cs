using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BorjiShop.Models;
using BorjiShop.BLL;
using BorjiShop.DAL;
namespace BorjiShop.Areas.Admin.Controllers
{
    public class PieceTypeController : Controller
    {
        private BLL_PieceType db = new BLL_PieceType();
        // GET: Admin/PieceType
        public ActionResult Index()
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            return View();
        }

        [HttpPost]
        public ActionResult GetPieceTypes(int page)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = db.GetPieceTypeList(page);
            var lastPage = db.LastPageNumber();
            object finalResult = new Tuple<List<Piecetype>, int, int>(result, page, lastPage);
            return Json(finalResult);
        }

        public ActionResult AddPieceType(Piecetype pieceType)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = "";
            if (!db.ExistPieceType(pieceType))
            {
                result = pieceType == null ? "نوع قطعه نظر معتبر نمی باشد." : db.AddPieceType(pieceType);
            }
            else
            {
                result = "نوع قطعه مورد نظر قبلاً ثبت شده است.";
            }

            var lastPage = db.LastPageNumber();
            object finalResult = new Tuple<string, int>(result, lastPage);
            return Json(finalResult);
        }

        public ActionResult UpdatePieceType(int ID, string EnglishName, string PersianName, int page)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var piecetype = new Piecetype();
            piecetype.EnglishName = EnglishName;
            piecetype.PersianName = PersianName;

            var result = "";
            if (db.ExistPieceType(ID))
            {
                if (db.ExistPieceType(piecetype))
                {
                    result = "نوع قطعه مورد نظر قبلاً ثبت شده است.";
                }
                else
                    result = db.UpdatePieceType(ID, EnglishName, PersianName);
            }
            else
            {
                result = "نوع قطعه مورد نظر معتبر نیست.";
            }

            object finalResult = new Tuple<string, int>(result, page);
            return Json(finalResult);
        }

        public ActionResult DeletePieceType(int ID)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = "";
            if (db.ExistPieceType(ID))
            {
                result = db.DeletePieceType(ID);
            }
            else
            {
                result = "نوع قطعه مورد نظر معتبر نیست.";
            }
            var lastPage = db.LastPageNumber();
            object finalResult = new Tuple<string, int>(result, lastPage);
            return Json(finalResult);
        }
    }
}