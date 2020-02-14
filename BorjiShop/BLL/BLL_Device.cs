using System.Collections.Generic;
using BorjiShop.DAL;
using BorjiShop.Models;

namespace BorjiShop.BLL
{
    public class BLL_Device
    {
        private DAL_Device dal_Device = new DAL_Device();

        public List<Device> GetDeviceList(int page)
        {
            return dal_Device.GetDevicesList(page);
        }

        public string AddDevice(Device device)
        {
            return dal_Device.AddDevice(device);
        }

        public bool ExistDevice(Device device)
        {
            return dal_Device.ExistDevice(device);
        }

        public bool ExistDevice(int ID)
        {
            return dal_Device.ExistDevice(ID);
        }

        public int LastPageNumber()
        {
            return dal_Device.LastPageNumber();
        }

        public string UpdateDevice(int ID, string englishName, string persianName, int brandId)
        {
            return dal_Device.UpdateDevice(ID, englishName, persianName, brandId);
        }

        public string DeleteDevice(int ID)
        {
            return dal_Device.DeleteDevice(ID);
        }

        public List<Device> GetAllDevice()
        {
            return dal_Device.GetAllDevice();
        }
    }
}