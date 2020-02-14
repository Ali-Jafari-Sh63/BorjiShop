using System;
using System.Collections.Generic;
using System.Linq;
using BorjiShop.Models;
using WebGrease.Css.Ast.Selectors;

namespace BorjiShop.DAL
{
    public class DAL_Piece
    {
        private BorjiShopContext db = new BorjiShopContext();
        private int _pageCapacity = 15;
        public List<Piece> GetPiecesList(int page)
        {
            var result = db.Pieces.OrderByDescending(d => d.ID).Skip((page - 1) * _pageCapacity).Take(_pageCapacity).ToList();
            return result;
        }

        public List<Piece> GetPiecesListByDeviceList(int id, int page)
        {
            var result = (from p in db.Pieces
                          where p.DeviceId == id
                          select p).OrderByDescending(p=>p.ID).Skip((page - 1) * _pageCapacity).Take(_pageCapacity).ToList();
            return result;
        }

        public string AddPiece(Piece piece, string keys)
        {
            string result;
            int pieceId;
            try
            {
                db.Pieces.Add(piece);
                db.SaveChanges();
                pieceId = piece.ID;
                if (!string.IsNullOrEmpty(keys))
                {
                    string[] newKeys = keys.Split('-');
                    foreach (var key in newKeys)
                    {
                        PieceKey pieceKey = new PieceKey()
                        {
                            PieceId = pieceId,
                            Key = key.Trim()
                        };
                        db.PieceKeys.Add(pieceKey);
                        db.SaveChanges();
                    }
                }
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public string UpdatePiece(int ID, int brandId, int deviceId, int pieceTypeId, int Price, bool IsSlider, string keys)
        {
            string result;
            try
            {
                var piece = GetPiece(ID);
                piece.BrandId = brandId;
                piece.DeviceId = deviceId;
                piece.PieceTypeId = pieceTypeId;
                piece.Price = Price;
                piece.IsSlider = IsSlider;
                db.SaveChanges();
                RemoveKeys(ID);
                if (!string.IsNullOrEmpty(keys))
                {
                    string[] newKeys = keys.Split('-');
                    foreach (var key in newKeys)
                    {
                        PieceKey pieceKey = new PieceKey()
                        {
                            PieceId = ID,
                            Key = key.Trim()
                        };
                        db.PieceKeys.Add(pieceKey);
                        db.SaveChanges();
                    }
                }

                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public string DeletePiece(int ID)
        {
            string result;
            try
            {
                var piece = GetPiece(ID);
                if (piece != null)
                {
                    db.Pieces.Remove(piece);
                    db.SaveChanges();
                    RemoveKeys(ID);
                    result = "Success";
                }
                else
                {
                    result = "قطعه مورد نظر معتبر نیست.";
                }
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public Piece GetPiece(Piece piece)
        {
            var result = db.Pieces
                .FirstOrDefault(d => d.BrandId == piece.BrandId && d.DeviceId == piece.DeviceId && d.PieceTypeId == piece.PieceTypeId && d.Price == piece.Price);
            return result;
        }

        public Piece GetPiece(int ID)
        {
            var result = db.Pieces
                .FirstOrDefault(d => d.ID == ID);
            return result;
        }

        public bool ExistPiece(Piece piece)
        {
            var existPiece = GetPiece(piece);
            return (existPiece != null);
        }

        public bool ExistPiece(int ID)
        {
            var existPiece = GetPiece(ID);
            return (existPiece != null);
        }

        public int LastPageNumber()
        {
            var countAllDevice = db.Pieces.ToList().Count();
            var lastPage = countAllDevice % _pageCapacity == 0
                ? (countAllDevice / _pageCapacity)
                : (countAllDevice / _pageCapacity) + 1;
            lastPage = lastPage == 0 ? 1 : lastPage;
            return lastPage;
        }

        public string UpdateFileName(int id, string fileName)
        {
            string result;
            try
            {
                var piece = GetPiece(id);
                piece.FileName = fileName;
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public List<Piece> GetAllPiece()
        {
            var result = db.Pieces.OrderByDescending(b => b.ID).ToList();
            return result;
        }

        public List<Piece> SearchPiece(int pieceTypeId, int brandId, int deviceId, int page)
        {
            List<Piece> result = new List<Piece>();
            result = (from p in db.Pieces
                      where (p.PieceTypeId == pieceTypeId || pieceTypeId == 0)
                              && (p.BrandId == brandId || brandId == 0)
                             && (p.DeviceId == deviceId || deviceId == 0)
                      select p).OrderByDescending(p => p.ID).Skip((page - 1) * _pageCapacity).Take(_pageCapacity).ToList();
            return result;
        }

        public int SearchPiecesLastPage(int pieceTypeId, int brandId, int deviceId)
        {
            var result = (from p in db.Pieces
                          where (p.PieceTypeId == pieceTypeId || pieceTypeId == 0)
                                && (p.BrandId == brandId || brandId == 0)
                                && (p.DeviceId == deviceId || deviceId == 0)
                          select p).ToList().Count();
            var lastPage = result % _pageCapacity == 0
                ? (result / _pageCapacity)
                : (result / _pageCapacity) + 1;
            return lastPage;
        }

        public void RemoveKeys(int pieceId)
        {
            var keys = GetKeys(pieceId);
            try
            {
                db.PieceKeys.RemoveRange(keys);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PieceKey> GetKeys(int pieceId)
        {
           return (from k in db.PieceKeys where k.PieceId == pieceId select k).ToList();
        }
    }
}