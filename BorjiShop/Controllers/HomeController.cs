using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BorjiShop.BLL;
using BorjiShop.DAL;
using BorjiShop.Models;

namespace BorjiShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int page = 1)
        {
            if (Request.Cookies["BorjiShop"] != null)
            {
                string rememberMe = Request.Cookies["BorjiShop"].Value;
                if (!string.IsNullOrEmpty(rememberMe))
                {
                    var user = new BLL_User().ExistRememberMe(rememberMe);
                    if (user != null)
                    {
                        Session["User"] = user;
                    }
                }
            }

            BLL_Brand dbBrand = new BLL_Brand();
            BLL_Piece dbPiece = new BLL_Piece();
            BLL_Device dbDevice = new BLL_Device();
            BLL_PieceType dbPieceType = new BLL_PieceType();

            var brandList = dbBrand.GetAllBrand();
            var deviceList = dbDevice.GetAllDevice();
            var pieceTypeList = dbPieceType.GetAllPieceType();

            ViewBag.Brands = dbBrand.GetAllBrand();
            ViewBag.Devices = deviceList;
            ViewBag.PieceType = pieceTypeList;
            List<Piece> slider = dbPiece.GetAllPiece().Where(p => p.IsSlider).Take(5).ToList();
            var finalSlider = new List<FinalPiece>();
            foreach (var piece in slider)
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
                finalSlider.Add(p);
            }
            ViewBag.Slider = finalSlider;

            List<Piece> pieces = dbPiece.GetPiecsList(page).ToList();
            var finalPieces = new List<FinalPiece>();
            foreach (var piece in pieces)
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
                p.Date = PublicFunction.ConvertMiladiToShamsi(piece.CreateDate);
                p.IsSlider = piece.IsSlider;
                finalPieces.Add(p);
            }
            ViewBag.Pieces = finalPieces;
            ViewBag.Page = page;
            ViewBag.LastPage = dbPiece.LastPageNumber();
            return View();
        }

        public ActionResult SearchPiece(int pieceTypeId, int brandId, int deviceId, int page)
        {
            BLL_Brand dbBrand = new BLL_Brand();
            BLL_Piece dbPiece = new BLL_Piece();
            BLL_Device dbDevice = new BLL_Device();
            BLL_PieceType dbPieceType = new BLL_PieceType();

            var brandList = dbBrand.GetAllBrand();
            var deviceList = dbDevice.GetAllDevice();
            var pieceTypeList = dbPieceType.GetAllPieceType();

            List<Piece> pieces = dbPiece.SearchPiece(pieceTypeId, brandId, deviceId, page).ToList();
            var finalPieces = new List<FinalPiece>();
            foreach (var piece in pieces)
            {
                FinalPiece p = new FinalPiece
                {
                    ID = piece.ID,
                    BrandId = piece.BrandId,
                    BrandName = brandList.FirstOrDefault(b => b.ID == piece.BrandId)?.EnglishName,
                    DeviceId = piece.DeviceId,
                    DeviceName = deviceList.FirstOrDefault(d => d.ID == piece.DeviceId)?.EnglishName,
                    PieceTypeId = piece.PieceTypeId,
                    PieceTypeName = pieceTypeList.FirstOrDefault(d => d.ID == piece.PieceTypeId)?.PersianName,
                    Price = piece.Price,
                    FileName = piece.FileName,
                    Date = PublicFunction.ConvertMiladiToShamsi(piece.CreateDate),
                    IsSlider = piece.IsSlider
                };
                finalPieces.Add(p);
            }

            var lastPage = dbPiece.SearchPiecesLastPage(pieceTypeId, brandId, deviceId);
            string brandName = "";
            brandName = brandId == 0 ? "" : dbBrand.GetAllBrand().Find(b => b.ID == brandId).PersianName;

            object finalResult = new Tuple<List<FinalPiece>, int, int, List<Brand>, List<Device>, List<Piecetype>, string>(finalPieces, page, lastPage, brandList, deviceList, pieceTypeList, brandName);

            return Json(finalResult);
        }

        public ActionResult SearchPiece2(int pieceTypeId, int brandId, int deviceId, int page)
        {
            return View("Index");
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

        public ActionResult IndexByDeviceId(int id, int page)
        {
            //if (Request.Cookies["BorjiShop"] != null)
            //{
            //    string rememberMe = Request.Cookies["BorjiShop"].Value;
            //    if (!string.IsNullOrEmpty(rememberMe))
            //    {
            //        var user = new BLL_User().ExistRememberMe(rememberMe);
            //        if (user != null)
            //        {
            //            Session["User"] = user;
            //        }
            //    }
            //}

            BLL_Brand dbBrand = new BLL_Brand();
            BLL_Piece dbPiece = new BLL_Piece();
            BLL_Device dbDevice = new BLL_Device();
            BLL_PieceType dbPieceType = new BLL_PieceType();

            var brandList = dbBrand.GetAllBrand();
            var deviceList = dbDevice.GetAllDevice();
            var pieceTypeList = dbPieceType.GetAllPieceType();

            ViewBag.Brands = dbBrand.GetAllBrand();
            ViewBag.Devices = deviceList;
            ViewBag.PieceType = pieceTypeList;
            List<Piece> slider = dbPiece.GetAllPiece().Where(p => p.IsSlider).Take(5).ToList();
            var finalSlider = new List<FinalPiece>();
            foreach (var piece in slider)
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
                finalSlider.Add(p);
            }
            ViewBag.Slider = finalSlider;

            List<Piece> pieces = dbPiece.GetPiecesListByDeviceList(id, page).ToList();
            var finalPieces = new List<FinalPiece>();
            foreach (var piece in pieces)
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
                p.Date = PublicFunction.ConvertMiladiToShamsi(piece.CreateDate);
                p.IsSlider = piece.IsSlider;
                finalPieces.Add(p);
            }
            ViewBag.Pieces = finalPieces;
            ViewBag.Page = page;
            ViewBag.LastPage = dbPiece.LastPageNumber();
            return View("Index");
        }
    }
}