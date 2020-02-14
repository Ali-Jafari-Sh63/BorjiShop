using System;
using System.Collections.Generic;
using System.Linq;
using BorjiShop.Models;

namespace BorjiShop.DAL
{
    public class DAL_Brand
    {
        private BorjiShopContext db = new BorjiShopContext();
        private int _pageCapacity = 15;

        public List<Brand> GetAllBrand()
        {
            var result = db.Brands.OrderBy(b => b.ID).ToList();
            return result;
        }

        public List<Brand> GetBrandList(int page)
        {
            var result = db.Brands.OrderBy(b => b.ID).Skip((page - 1) * _pageCapacity).Take(_pageCapacity).ToList();
            return result;
        }

        public string AddBrand(Brand brand)
        {
            string result;
            try
            {
                db.Brands.Add(brand);
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public string UpdateBrand(int ID, string englishName, string persianName)
        {
            string result;
            try
            {
                var brand = GetBrand(ID);
                brand.EnglishName = englishName;
                brand.PersianName = persianName;
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public string DeleteBrand(int ID)
        {
            string result;
            try
            {
                var brand = GetBrand(ID);
                if (brand != null)
                {
                    db.Brands.Remove(brand);
                    db.SaveChanges();
                    result = "Success";
                }
                else
                {
                    result = "برند مورد نظر معتبر نیست.";
                }
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public Brand GetBrand(Brand brand)
        {
            var result = db.Brands
                .FirstOrDefault(b => b.PersianName == brand.PersianName && b.EnglishName == brand.EnglishName);
            return result;
        }

        public Brand GetBrand(int ID)
        {
            var result = db.Brands
                .FirstOrDefault(b => b.ID == ID);
            return result;
        }

        public bool ExistBrand(Brand brand)
        {
            var existBrand = GetBrand(brand);
            return (existBrand != null);
        }

        public bool ExistBrand(int ID)
        {
            var existBrand = GetBrand(ID);
            return (existBrand != null);
        }

        public int LastPageNumber()
        {
            var countAllBrand = db.Brands.ToList().Count();
            var lastPage = countAllBrand % _pageCapacity == 0
                ? (countAllBrand / _pageCapacity)
                : (countAllBrand / _pageCapacity) + 1;
            return lastPage;
        }

    }
}