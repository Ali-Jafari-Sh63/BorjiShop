using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BorjiShop.Models;

namespace BorjiShop.DAL
{
    public class DAL_Us
    {
        private BorjiShopContext db = new BorjiShopContext();

        public string AddMessage(Message message)
        {
            string result;
            try
            {
                db.Messages.Add(message);
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }
    }
}