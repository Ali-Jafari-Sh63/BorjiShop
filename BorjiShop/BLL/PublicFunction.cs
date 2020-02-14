using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BorjiShop.BLL
{
    public static class PublicFunction
    {
        public static string ConvertMiladiToShamsi(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format(@"{0}/{1}/{2}",
                pc.GetYear(dt),
                pc.GetMonth(dt),
                pc.GetDayOfMonth(dt));
        }

        public static string HashPassword(string password)
        {
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytes = ue.GetBytes(password);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);
            string result;
            result = System.Text.RegularExpressions.Regex.Replace(BitConverter.ToString(hashBytes), "-", "").ToLower();
            return result;
        }

        public static bool CheckEmail(string email)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(email,
                @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"))
                return true;
            else
                return false;
        }

        public static string CalculatDate(DateTime value)
        {
            DateTime dtNow = DateTime.Now;

            TimeSpan dt = (dtNow - value);


            string text = "";
            if (dt.Days >= 30)
            {
                if (dt.Days / 30 > 12)
                {
                    text = "بیش از 1 سال پیش";
                }
                else
                {
                    text = (dt.Days / 30).ToString() + " ماه قبل";
                }
                
            }
            else if (dt.Days < 30 && dt.Days > 0)
            {
                text = dt.Days + " روز قبل";
            }
            else if (dt.Days <= 0 && dt.Hours > 0)
            {
                text = dt.Hours + " ساعت قبل";
            }
            else if (dt.Days <= 0 && dt.Hours <= 0)
            {
                text = dt.Minutes + " دقیقه قبل";
            }
            return text;
        }
    }
}