using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BorjiShop.Models;
using BorjiShop.BLL;

namespace BorjiShop.Areas.Admin.Controllers
{
    public class DeviceController : Controller
    {
        private BLL_Device db = new BLL_Device();
        // GET: Admin/Device
        public ActionResult Index()
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            return View();
        }

        [HttpPost]
        public ActionResult GetDevices(int page)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = db.GetDeviceList(page);
            var lastPage = db.LastPageNumber();
            var brandList = new BLL_Brand().GetAllBrand();
            var finalDevice = new List<finalDevice>();
            foreach (var device in result)
            {
                finalDevice d = new finalDevice();
                d.ID = device.ID;
                d.EnglishName = device.EnglishName;
                d.PersianName = device.PersianName;
                d.BrandId = device.BrandId;
                d.BrandName = brandList.FirstOrDefault(b => b.ID == device.BrandId)?.EnglishName;
                finalDevice.Add(d);
            }
            object finalResult = new Tuple<List<finalDevice>, int, int, List<Brand>>(finalDevice, page, lastPage, brandList);
            return Json(finalResult);
        }

        public ActionResult AddDevice(Device device)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = "";
            if (!db.ExistDevice(device))
            {
                result = device == null ? "دستگاه مورد نظر معتبر نمی باشد." : db.AddDevice(device);
            }
            else
            {
                result = "دستگاه مورد نظر قبلاً ثبت شده است.";
            }

            var lastPage = db.LastPageNumber();
            object finalResult = new Tuple<string, int>(result, lastPage);
            return Json(finalResult);
        }

        public ActionResult UpdateDevice(int ID, int BrandId, string EnglishName, string PersianName, int page)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var device = new Device();
            device.EnglishName = EnglishName;
            device.PersianName = PersianName;
            device.BrandId = BrandId;
            var result = "";
            if (db.ExistDevice(ID))
            {
                if (db.ExistDevice(device))
                {
                    result = "دستگاه مورد نظر قبلاً ثبت شده است.";
                }
                else
                    result = db.UpdateDevice(ID, EnglishName, PersianName, BrandId);
            }
            else
            {
                result = "برند مورد نظر معتبر نیست.";
            }

            object finalResult = new Tuple<string, int>(result, page);
            return Json(finalResult);
        }

        public ActionResult DeleteDevice(int ID)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = "";
            if (db.ExistDevice(ID))
            {
                result = db.DeleteDevice(ID);
            }
            else
            {
                result = "دستگاه مورد نظر معتبر نیست.";
            }
            var lastPage = db.LastPageNumber();
            object finalResult = new Tuple<string, int>(result, lastPage);
            return Json(finalResult);
        }

        public ActionResult GetDeviceByBrandId(int brandId, int pieceId)
        {
            var result = db.GetAllDevice().Where(d => d.BrandId == brandId).ToList();
            if (pieceId != 0)
            {
                var piece = new BLL_Piece().GetAllPiece().FirstOrDefault(p => p.ID == pieceId);
                var deviceId = piece.DeviceId;
                object finalResult = new Tuple<List<Device>, int>(result, deviceId);
                return Json(finalResult);
            }
            else
            {
                object finalResult = new Tuple<List<Device>, int>(result, 0);
                return Json(finalResult);
            }

        }
        public class finalDevice
        {
            public int ID { get; set; }
            public string EnglishName { get; set; }
            public string PersianName { get; set; }
            public int BrandId { get; set; }
            public string BrandName { get; set; }
        }
    }
}