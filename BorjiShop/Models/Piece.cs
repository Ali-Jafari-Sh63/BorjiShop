using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BorjiShop.Models
{
    public class Piece
    {
        public int ID { get; set; }

        public int BrandId { get; set; }

        public int DeviceId { get; set; }

        public int PieceTypeId { get; set; }

        public int Price { get; set; }

        public string FileName { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsSlider { get; set; }

        public bool IsSold { get; set; }

        public DateTime? SaleDate { get; set; }

        public int BuyerID { get; set; }

        public ICollection<PieceKey> PieceKeys { get; set; }
    }
}