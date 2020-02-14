using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjiShop.Models
{
    public class UserRole
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string EnRoleName { get; set; }

        [Required]
        public string PeRoleName { get; set; }
    }
}