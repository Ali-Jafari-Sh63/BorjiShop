using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using BorjiShop.Models;
using BorjiShop.BLL;

namespace BorjiShop.Areas.Admin.Controllers
{
    public class MessageController : Controller
    {
        private BLL_Message db = new BLL_Message();
        // GET: Admin/Message
        public ActionResult Index(int page = 1)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            var result = db.GetAllMessages(page);
            ViewBag.LastPage = db.LastPageNumber();
            ViewBag.CurrentPage = page;
            return View(result);
        }

        public ActionResult ReadMessage(int id, int page)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            db.ReadMessage(id);
            var message = db.GetMessage(id);
            ViewBag.CurrentPage = page;
            return View(message);
        }

        public ActionResult ReplyMessage(Message message)
        {
            var result = "";
            if (Session["AdminUser"] == null)
                return RedirectToAction("Login", "User");
            if (string.IsNullOrEmpty(message.Answer))
            {
                result = "لطفاً ابتدا پاسخ را وارد نمایید.";
            }
            else
            {
                result = db.ReplyMessage(message.ID, message.Answer);
                if (result == "Success")
                {
                    Message m = db.GetMessage(message.ID);
                    string fullname = m.FullName;
                    string email = m.Email;
                    SendEmail(email, fullname, message.Answer);
                    db.TickAnswered(message.ID);

                }
            }
            return Json(result);
        }

        public void SendEmail(string email, String fullName, string reply)
        {
            MailMessage MyEmail = new MailMessage();
            string body = string.Empty;
            using (StreamReader reader =
                new StreamReader(HttpContext.Server.MapPath("~/Areas/Admin/Views/Message/AnswerTemplate.html")))
            {

                body = reader.ReadToEnd();
            }
            //string serverAddress = ConfigUtility.GetValue(p => p.ServerAddress);
            MyEmail.Subject = "پاسخ فروشگاه برجی";



            string myDate = PublicFunction.ConvertMiladiToShamsi(DateTime.Now);
            body = body.Replace("{date}", myDate);
            body = body.Replace("{fullname}", fullName);
            body = body.Replace("{body}", reply);
            MyEmail.To.Add(email);
            MyEmail.BodyEncoding = Encoding.UTF8;
            MyEmail.Body = body;
            MyEmail.IsBodyHtml = true;
            MyEmail.SubjectEncoding = Encoding.UTF8;
            MyEmail.Priority = MailPriority.High;
            MyEmail.DeliveryNotificationOptions = DeliveryNotificationOptions.Never;

            SmtpClient mySmtp = new SmtpClient();
            mySmtp.Timeout = 100000;
            mySmtp.EnableSsl = true;
            mySmtp.Send(MyEmail);
        }
    }
}