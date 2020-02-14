using System.Collections.Generic;
using BorjiShop.DAL;
using BorjiShop.Models;

namespace BorjiShop.BLL
{
    public class BLL_Piece
    {
        private DAL_Piece dal_Piece = new DAL_Piece();

        public List<Piece> GetPiecsList(int page)
        {
            return dal_Piece.GetPiecesList(page);
        }

        public List<Piece> GetPiecesListByDeviceList(int id, int page)
        {
            return dal_Piece.GetPiecesListByDeviceList(id, page);
        }

        public string AddPiece(Piece piece, string keys)
        {
            return dal_Piece.AddPiece(piece, keys);
        }

        public bool ExistPiece(Piece piece)
        {
            return dal_Piece.ExistPiece(piece);
        }

        public bool ExistPiece(int ID)
        {
            return dal_Piece.ExistPiece(ID);
        }

        public int LastPageNumber()
        {
            return dal_Piece.LastPageNumber();
        }

        public string UpdatePiece(int ID, int brandId, int deviceId, int pieceTypeId, int price, bool isSlider , string keys)
        {
            return dal_Piece.UpdatePiece(ID, brandId, deviceId, pieceTypeId, price, isSlider,keys);
        }

        public string DeletePiece(int ID)
        {
            return dal_Piece.DeletePiece(ID);
        }

        public string UpdateFileName(int id, string fileName)
        {
            return dal_Piece.UpdateFileName(id, fileName);
        }

        public List<Piece> GetAllPiece()
        {
            return dal_Piece.GetAllPiece();
        }

        public Piece GetPiece(int ID)
        {
            return dal_Piece.GetPiece(ID);
        }

        public List<Piece> SearchPiece(int pieceTypeId, int brandId, int deviceId, int page)
        {
            return dal_Piece.SearchPiece(pieceTypeId, brandId, deviceId, page);
        }

        public int SearchPiecesLastPage(int pieceTypeId, int brandId, int deviceId)
        {
            return dal_Piece.SearchPiecesLastPage(pieceTypeId, brandId, deviceId);
        }

        public List<PieceKey> GetKeys(int pieceId)
        {
            return dal_Piece.GetKeys(pieceId);
        }
    }
}