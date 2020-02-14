using System.Collections.Generic;
using BorjiShop.DAL;
using BorjiShop.Models;

namespace BorjiShop.BLL
{
    public class BLL_Brand
    {
        private DAL_Brand dal_Brand = new DAL_Brand();
        public List<Brand> GetBrandList(int page)
        {
            return dal_Brand.GetBrandList(page);
        }

        public string AddBrand(Brand brand)
        {
            return dal_Brand.AddBrand(brand);
        }

        public bool ExistBrand(Brand brand)
        {
            return dal_Brand.ExistBrand(brand);
        }

        public bool ExistBrand(int ID)
        {
            return dal_Brand.ExistBrand(ID);
        }

        public int LastPageNumber()
        {
            return dal_Brand.LastPageNumber();
        }

        public string UpdateBrand(int ID, string englishName, string persianName)
        {
            return dal_Brand.UpdateBrand(ID, englishName, persianName);
        }

        public string DeleteBrand(int ID)
        {
            return dal_Brand.DeleteBrand(ID);
        }

        public List<Brand> GetAllBrand()
        {
           return dal_Brand.GetAllBrand();
        }
    }
}