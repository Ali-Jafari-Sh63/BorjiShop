using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjiShop.Models
{
    public class PieceKey
    {
        [Key]
        public int Id { get; set; }

        public int PieceId { get; set; }

        public string Key { get; set; }
    }
}