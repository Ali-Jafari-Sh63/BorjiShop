using System;
using System.Collections.Generic;
using System.Linq;
using BorjiShop.Models;

namespace BorjiShop.DAL
{
    public class DAL_PieceType
    {
        private BorjiShopContext db = new BorjiShopContext();
        private int _pageCapacity = 15;

        public List<Piecetype> GetAllPieceType()
        {
            var result = db.Piecetypes.OrderBy(b => b.ID).ToList();
            return result;
        }

        public List<Piecetype> GetPieceTypeList(int page)
        {
            var result = db.Piecetypes.OrderBy(d => d.ID).Skip((page - 1) * _pageCapacity).Take(_pageCapacity).ToList();
            return result;
        }

        public string AddPieceType(Piecetype pieceType)
        {
            string result;
            try
            {
                db.Piecetypes.Add(pieceType);
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public string UpdatePieceType(int ID, string englishName, string persianName)
        {
            string result;
            try
            {
                var pieceType = GetPieceType(ID);
                pieceType.EnglishName = englishName;
                pieceType.PersianName = persianName;
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public string DeletePieceType(int ID)
        {
            string result;
            try
            {
                var pieceType = GetPieceType(ID);
                if (pieceType != null)
                {
                    db.Piecetypes.Remove(pieceType);
                    db.SaveChanges();
                    result = "Success";
                }
                else
                {
                    result = "نوع قطعه مورد نظر معتبر نیست.";
                }
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public Piecetype GetPieceType(Piecetype pieceType)
        {
            var result = db.Piecetypes
                .FirstOrDefault(d => d.EnglishName == pieceType.EnglishName && d.PersianName == pieceType.PersianName);
            return result;
        }

        public Piecetype GetPieceType(int ID)
        {
            var result = db.Piecetypes
                .FirstOrDefault(d => d.ID == ID);
            return result;
        }

        public bool ExistPieceType(Piecetype Piecetype)
        {
            var existPieceType = GetPieceType(Piecetype);
            return (existPieceType != null);
        }

        public bool ExistPieceType(int ID)
        {
            var existPieceType = GetPieceType(ID);
            return (existPieceType != null);
        }

        public int LastPageNumber()
        {
            var countAllPieceType = db.Piecetypes.ToList().Count();
            var lastPage = countAllPieceType % _pageCapacity == 0
                ? (countAllPieceType / _pageCapacity)
                : (countAllPieceType / _pageCapacity) + 1;
            return lastPage;
        }
    }
}