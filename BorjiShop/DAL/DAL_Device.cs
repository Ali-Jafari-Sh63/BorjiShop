using System;
using System.Collections.Generic;
using System.Linq;
using BorjiShop.Models;

namespace BorjiShop.DAL
{
    public class DAL_Device
    {
        private BorjiShopContext db = new BorjiShopContext();
        private int _pageCapacity = 15;

        public List<Device> GetAllDevice()
        {
            var result = db.Devices.OrderBy(b => b.ID).ToList();
            return result;
        }

        public List<Device> GetDevicesList(int page)
        {
            var result = db.Devices.OrderBy(d => d.ID).Skip((page - 1) * _pageCapacity).Take(_pageCapacity).ToList();
            return result;
        }

        public string AddDevice(Device device)
        {
            string result;
            try
            {
                db.Devices.Add(device);
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public string UpdateDevice(int ID, string englishName, string persianName, int brandId)
        {
            string result;
            try
            {
                var device = GetDevice(ID);
                device.EnglishName = englishName;
                device.PersianName = persianName;
                device.BrandId = brandId;
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public string DeleteDevice(int ID)
        {
            string result;
            try
            {
                var device = GetDevice(ID);
                if (device != null)
                {
                    db.Devices.Remove(device);
                    db.SaveChanges();
                    result = "Success";
                }
                else
                {
                    result = "دستگاه مورد نظر معتبر نیست.";
                }
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public Device GetDevice(Device device)
        {
            var result = db.Devices
                .FirstOrDefault(d => d.EnglishName == device.EnglishName && d.PersianName == device.PersianName && d.BrandId == device.BrandId);
            return result;
        }

        public Device GetDevice(int ID)
        {
            var result = db.Devices
                .FirstOrDefault(d => d.ID == ID);
            return result;
        }

        public bool ExistDevice(Device device)
        {
            var existDevice = GetDevice(device);
            return (existDevice != null);
        }

        public bool ExistDevice(int ID)
        {
            var existDevice = GetDevice(ID);
            return (existDevice != null);
        }

        public int LastPageNumber()
        {
            var countAllDevice = db.Devices.ToList().Count();
            var lastPage = countAllDevice % _pageCapacity == 0
                ? (countAllDevice / _pageCapacity)
                : (countAllDevice / _pageCapacity) + 1;
            return lastPage;
        }
    }
}