using System.Collections.Generic;


namespace BorjiShop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int PieceId { get; set; }    

    }
}