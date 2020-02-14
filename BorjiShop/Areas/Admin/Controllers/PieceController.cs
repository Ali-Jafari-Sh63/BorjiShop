using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using BorjiShop.Models;
using BorjiShop.BLL;
using BorjiShop.DAL;

namespace BorjiShop.Areas.Admin.Controllers
{
    public class PieceController : Controller
    {
        private BLL_Piece db = new BLL_Piece();
        // GET: Admin/Piece
        public ActionResult Index(int page)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            ViewBag.Page = page;
            return View();
        }

        [HttpPost]
        public ActionResult GetPieces(int page)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = db.GetPiecsList(page);
            var lastPage = db.LastPageNumber();
            var brandList = new BLL_Brand().GetAllBrand();
            var deviceList = new BLL_Device().GetAllDevice();
            var pieceTypeList = new BLL_PieceType().GetAllPieceType();
            var finalPieces = new List<FinalPiece>();
            foreach (var piece in result)
            {
                FinalPiece p = new FinalPiece();
                p.ID = piece.ID;
                p.BrandId = piece.BrandId;
                p.BrandName = brandList.FirstOrDefault(b => b.ID == piece.BrandId)?.EnglishName;
                p.DeviceId = piece.DeviceId;
                p.DeviceName = deviceList.FirstOrDefault(d => d.ID == piece.DeviceId)?.EnglishName;
                p.PieceTypeId = piece.PieceTypeId;
                p.PieceTypeName = pieceTypeList.FirstOrDefault(d => d.ID == piece.PieceTypeId)?.PersianName;
                p.Price = piece.Price;
                p.FileName = piece.FileName;
                //p.Date = piece.Date.ToString("yyyy/MM/dd");
                p.Date = PublicFunction.ConvertMiladiToShamsi(piece.CreateDate);
                p.IsSlider = piece.IsSlider;
                finalPieces.Add(p);
            }
            object finalResult = new Tuple<List<FinalPiece>, int, int, List<Brand>, List<Device>, List<Piecetype>>(finalPieces, page, lastPage, brandList, deviceList, pieceTypeList);
            return Json(finalResult);
        }
        [HttpGet]
        public ActionResult Modify(int type, int ID, int? page)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            if (type != 0 && type != 1)
                return RedirectToAction("Index");
            if (type == 1 && ID == 0)
                return RedirectToAction("Index");
            var bll_PieceType = new BLL_PieceType();
            var bll_Brand = new BLL_Brand();
            var bll_Device = new BLL_Device();
            ViewBag.PieceTypes = bll_PieceType.GetAllPieceType();
            ViewBag.Brands = bll_Brand.GetAllBrand();
            ViewBag.Devices = bll_Device.GetAllDevice();
            ViewBag.Type = type;
            ViewBag.Id = ID;
            if (type == 1)
            {
                ViewBag.Piece = db.GetAllPiece().FirstOrDefault(p => p.ID == ID);
                List<PieceKey> keys = db.GetKeys(ID);
                string pieceKeys = "";
                foreach (var pieceKey in keys)
                {
                    pieceKeys += pieceKey.Key + " -";
                }

                if (pieceKeys.EndsWith("-"))
                {
                    pieceKeys = pieceKeys.Substring(0, pieceKeys.Length - 1);
                }

                ViewBag.Keys = pieceKeys;
            }

            if (page != null)
                ViewBag.Page = page;
            return View();
        }

        public ActionResult ModifyPiece(Piece piece,string pieceKeys, int type, int id)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            if (type != 0 && type != 1)
                return RedirectToAction("Index");
            if (type == 1 && id == 0)
                return RedirectToAction("Index");
            var result = "";
            object finalResult = new object();
            if (type == 0 && id == 0)
            {
                piece.CreateDate = DateTime.Now.Date;
                result = db.AddPiece(piece, pieceKeys);
                var lastPage = db.LastPageNumber();
                finalResult = new Tuple<string, int, string, int>(result, piece.ID,
                    piece.ID.ToString() + piece.CreateDate.ToShortDateString().Replace("/", string.Empty), lastPage);
            }

            if (type == 1)
            {
                result = db.UpdatePiece(id, piece.BrandId, piece.DeviceId, piece.PieceTypeId, piece.Price, piece.IsSlider, pieceKeys);
                var editedPiece = db.GetPiece(id);
                finalResult = new Tuple<string, int, string>(result, editedPiece.ID, editedPiece.ID.ToString() + editedPiece.CreateDate.ToShortDateString().Replace("/", string.Empty));
            }
            return Json(finalResult);
        }

        public ActionResult UploadPicture(string FileName, int ID)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            object result;
            try
            {
                if (Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = Request.Files["UploadedImage"];
                    var fileName = FileName;

                    if (httpPostedFile != null)
                    {
                        string fileSavePath = Server.MapPath("/PieceImage/" + FileName + "." + httpPostedFile.FileName.Split('.')[1]);

                        string fileDeletePath = Directory.GetFiles(Server.MapPath("/PieceImage/")).FirstOrDefault(c => Path.GetFileNameWithoutExtension(c) == FileName);
                        if (fileDeletePath != null && System.IO.File.Exists(fileDeletePath))
                            System.IO.File.Delete(fileDeletePath);
                        httpPostedFile.SaveAs(fileSavePath);


                        db.UpdateFileName(ID, FileName + "." + httpPostedFile.FileName.Split('.')[1]);
                    }
                    else { }
                }
                else { }
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return Content(result.ToString());
        }

        public ActionResult DeletePiece(int ID)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = "";
            if (db.ExistPiece(ID))
            {
                Piece piece = db.GetPiece(ID);
                string FileName = piece.FileName.Split('.')[0];
                result = db.DeletePiece(ID);
                if (result == "Success")
                {
                    string fileDeletePath = Directory.GetFiles(Server.MapPath("/PieceImage/")).FirstOrDefault(c => Path.GetFileNameWithoutExtension(c) == FileName);
                    if (fileDeletePath != null && System.IO.File.Exists(fileDeletePath))
                        System.IO.File.Delete(fileDeletePath);
                }
            }
            else
            {
                result = "قطعه مورد نظر معتبر نیست.";
            }
            var lastPage = db.LastPageNumber();
            object finalResult = new Tuple<string, int>(result, lastPage);
            return Json(finalResult);
        }

        public class FinalPiece
        {
            public int ID { get; set; }
            public int BrandId { get; set; }
            public string BrandName { get; set; }
            public int DeviceId { get; set; }
            public string DeviceName { get; set; }
            public int PieceTypeId { get; set; }
            public string PieceTypeName { get; set; }
            public int Price { get; set; }
            public string FileName { get; set; }
            public string Date { get; set; }
            public bool IsSlider { get; set; }
        }


    }
}