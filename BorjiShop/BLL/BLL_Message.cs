using System.Collections.Generic;
using BorjiShop.DAL;
using BorjiShop.Models;

namespace BorjiShop.BLL
{
    public class BLL_Message
    {
        private DAL_Message dal_Message = new DAL_Message();
        public List<Message> GetAllMessages(int page)
        {
            return dal_Message.GetAllMessages(page);
        }

        public Message GetMessage(int id)
        {
            return dal_Message.GetMessage(id);
        }

        public void ReadMessage(int id)
        {
            dal_Message.ReadMessage(id);
        }

        public string ReplyMessage(int id, string answer)
        {
            return dal_Message.ReplyMessage(id, answer);
        }

        public string GetEmailAddress(int id)
        {
            return dal_Message.GetEmailAddress(id);
        }

        public void TickAnswered(int id)
        {
            dal_Message.TickAnswered(id);
        }

        public int LastPageNumber()
        {
            return dal_Message.LastPageNumber();
        }
    }
}