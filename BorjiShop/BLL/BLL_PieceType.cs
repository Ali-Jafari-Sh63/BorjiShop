using System.Collections.Generic;
using BorjiShop.DAL;
using BorjiShop.Models;

namespace BorjiShop.BLL
{
    public class BLL_PieceType
    {
        private DAL_PieceType dal_pieceType = new DAL_PieceType();

        public List<Piecetype> GetPieceTypeList(int page)
        {
            return dal_pieceType.GetPieceTypeList(page);
        }

        public string AddPieceType(Piecetype pieceType)
        {
            return dal_pieceType.AddPieceType(pieceType);
        }

        public bool ExistPieceType(Piecetype pieceType)
        {
            return dal_pieceType.ExistPieceType(pieceType);
        }

        public bool ExistPieceType(int ID)
        {
            return dal_pieceType.ExistPieceType(ID);
        }

        public int LastPageNumber()
        {
            return dal_pieceType.LastPageNumber();
        }

        public string UpdatePieceType(int ID, string englishName, string persianName)
        {
            return dal_pieceType.UpdatePieceType(ID, englishName, persianName);
        }

        public string DeletePieceType(int ID)
        {
            return dal_pieceType.DeletePieceType(ID);
        }

        public List<Piecetype> GetAllPieceType()
        {
            return dal_pieceType.GetAllPieceType();
        }
    }
}