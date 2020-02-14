using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BorjiShop.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public int UserRole { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string RememberMe { get; set; }
        public string ActivationCode { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }
}