using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BorjiShop.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Text { get; set; }

        public bool Read { get; set; }

        public bool Answered { get; set; }

        public DateTime Date { get; set; }

        public string Answer { get; set; }
    }
}