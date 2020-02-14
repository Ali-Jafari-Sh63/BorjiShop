using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BorjiShop.DAL;
using BorjiShop.Models;

namespace BorjiShop.BLL
{
    public class BLL_Us
    {
        private DAL_Us dal_Us = new DAL_Us();

        public string AddMessage(Message message)
        {
            return dal_Us.AddMessage(message);
        }
    }
}