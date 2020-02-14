using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BorjiShop.Models;

namespace BorjiShop.DAL
{
    public class DAL_Message
    {
        private readonly BorjiShopContext _db = new BorjiShopContext();
        private int _pageCapacity = 15;

        public List<Message> GetAllMessages(int page)
        {
            var result = _db.Messages.OrderByDescending(m => m.ID).Skip((page - 1) * _pageCapacity).Take(_pageCapacity).ToList();
            return result;
        }

        public Message GetMessage(int id)
        {
            var result = _db.Messages.FirstOrDefault(m => m.ID == id);
            return result;
        }

        public void ReadMessage(int id)
        {
            var message = GetMessage(id);
            message.Read = true;
            _db.SaveChanges();
        }

        public string ReplyMessage(int id, string answer)
        {
            string result;
            try
            {
                var message = GetMessage(id);
                message.Answer = answer;
                _db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return result;
        }

        public string GetEmailAddress(int id)
        {
            return GetMessage(id).Email;
        }

        public void TickAnswered(int id)
        {
            try
            {
                var message = GetMessage(id);
                message.Answered = true;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public int LastPageNumber()
        {
            var countAllMessage = _db.Messages.ToList().Count();
            var lastPage = countAllMessage % _pageCapacity == 0
                ? (countAllMessage / _pageCapacity)
                : (countAllMessage / _pageCapacity) + 1;
            lastPage = lastPage == 0 ? 1 : lastPage;
            return lastPage;
        }
    }
}