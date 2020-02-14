using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BorjiShop.Models;

namespace BorjiShop.DAL
{
    public class BorjiShopInitializer : DropCreateDatabaseIfModelChanges<BorjiShopContext>
    {
        protected override void Seed(BorjiShopContext context)
        {
        }
    }
}