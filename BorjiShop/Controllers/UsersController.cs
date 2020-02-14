using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BorjiShop.BLL;
using BorjiShop.Models;

namespace BorjiShop.Controllers
{
    public class UsersController : Controller
    {
        private BLL_User db = new BLL_User();
        // GET: Users
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult LoginUser(string email, string password, bool rememberMe)
        {
            var result = "";
            var stringMemeberMe = "";

            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
                result = "پست الکترونیک و رمز عبور را به درستی وارد نمایید.";
            else
            {
                string hashPassword = PublicFunction.HashPassword(password);
                password = PublicFunction.HashPassword(hashPassword);
                if (rememberMe)
                    stringMemeberMe = Guid.NewGuid().ToString().Replace("-", "");

                User user = new User()
                {
                    Email = email,
                    Password = password
                };

                string loginStatus = db.Login(user);

                if (loginStatus == "Success" && db.IsActiveUser(user) && !db.IsDeletedUser(user))
                {
                    Session["User"] = db.GetUser(email, password);
                    if (rememberMe)
                    {
                        db.AddRememberMe(user, stringMemeberMe);
                        Response.Cookies["BorjiShop"].Value = stringMemeberMe;
                        Response.Cookies["BorjiShop"].Expires = DateTime.Now.AddDays(10);
                    }
                    result = "Success";
                }
                else if (loginStatus == "Success" && !db.IsActiveUser(user))
                {
                    result = "کاربر محترم شما غیر فعال هستید. لطفاً با مدیریت فروشگاه تماس بگیرید.";
                }
                else if (loginStatus == "Success" && db.IsDeletedUser(user))
                {
                    result = "نام کاربری یا رمز عبور صحیح نیست.";
                }
                else if (loginStatus != "Success")
                {
                    result = loginStatus;
                }
                else
                {
                    result = "Error";
                }
            }
            return Json(result);
        }

        public ActionResult LogOutUser()
        {
            Session.Remove("User");
            if (Request.Cookies["BorjiShop"] != null)
            {
                Response.Cookies["BorjiShop"].Expires = DateTime.Now.AddDays(-1);
            }
            return Json(1);
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            var error = "";
            var result = "";
            if (user.FirstName == "")
            {
                error += "لطفا نام خود را وارد نمایید.";
                error = error + "</br>";
            }
            if (user.LastName == "")
            {
                error += "لطفا نام خانوادگی خود را وارد نمایید.</ br>";
                error = error + "</br>";
            }

            if (!PublicFunction.CheckEmail(user.Email))
            {
                error += "لطفاً پست الکترونیک معتبر وارد نمایید.";
                error = error + "</br>";
            }

            if (user.Phone == "")
            {
                error += "لطفا شماره تلفن خود را وارد نمایید.";
                error = error + "</br>";
            }

            if (user.Password == "")
            {
                error += "لطفا رمز عبور خود را وارد نمایید.";
                error = error + "</br>";
            }

            if (error != "")
            {
                result = error;
            }
            else
            {
                if (db.ExistUser(user))
                {
                    result = "پست الکترونیک قبلا ثبت شده است.‍";
                }
                else
                {
                    string hashPassword = PublicFunction.HashPassword(user.Password);
                    user.Password = PublicFunction.HashPassword(hashPassword);
                    string activationCode = Guid.NewGuid().ToString().Replace("-", "");
                    user.CreateDate = DateTime.Now;
                    user.IsActive = false;
                    user.IsDeleted = false;
                    user.UserRole = 2;
                    user.ActivationCode = activationCode;
                    result = db.AddUser(user);
                    if (result == "Success")
                    {
                        // send email
                        SendEmail(activationCode, "ActivationMail", user.Email);
                    }
                }
            }

            return Json(result);
        }

        public void SendEmail(string key, string emailType, string email)
        {
            MailMessage MyEmail = new MailMessage();
            string body = string.Empty;
            string url = string.Empty;
            if (emailType == "ActivationMail")
            {
                using (StreamReader reader =
                    new StreamReader(HttpContext.Server.MapPath("~/Views/Users/ActiveUserEmailTemplate.html")))
                {

                    body = reader.ReadToEnd();
                }
                //string serverAddress = ConfigUtility.GetValue(p => p.ServerAddress);
                //string url = serverAddress + "/FA/FormRecoveryPassword/ResetPassword?key=" + key;
                url = "http://localhost:51102/Users/ActiveUser?ActiveCode=" + key;
                MyEmail.Subject = "لینک فعال سازی کاربر";
            }


            string myDate = PublicFunction.ConvertMiladiToShamsi(DateTime.Now);
            body = body.Replace("{date}", myDate);
            body = body.Replace("{url}", url);
            body = body.Replace("{link}", url);
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

        public ActionResult ActiveUser(string ActiveCode)
        {
            string status = string.Empty;
            var user = db.GetUser(ActiveCode);
            if (user != null)
            {
                status = db.ActiveUser(user);
            }
            else
            {
                status = "Error";
            }

            ViewBag.Status = status;
            if (status == "Success")
            {
                ViewBag.User = user;
            }
            else
            {
                ViewBag.User = null;
            }

            return View();
        }

        //public void SearchCookie()
        //{
        //    if (Request.Cookies["BorjiShop"] != null)
        //    {
        //        string rememberMe = Request.Cookies["BorjiShop"].Value;
        //        if (!string.IsNullOrEmpty(rememberMe))
        //        {
        //            var user = db.ExistRememberMe(rememberMe);
        //            if (user != null)
        //            {
        //                Session["User"] = user;
        //            }
        //        }
        //    }
        //}
        public ActionResult UserProfile()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Home");
            var user = (User)Session["User"];
            var existuser = db.GetUser(user.Email, user.Password);
            ViewBag.User = existuser;
            return View();
        }

        public ActionResult UpdateProfile(User user)
        {
            if (user == null || Session["User"] == null)
                return RedirectToAction("Index", "Home");
            string result = string.Empty;
            if (string.IsNullOrEmpty(user.FirstName))
            {
                result = "Error";
            }
            if (string.IsNullOrEmpty(user.LastName))
            {
                result = "Error";
            }
            if (string.IsNullOrEmpty(user.Phone))
            {
                result = "Error";
            }

            if (string.IsNullOrEmpty(result))
            {
                var sUser = (User)Session["User"];
                result = db.UpdateUser(sUser.ID, user.FirstName, user.LastName, user.Phone);
                Session["User"] = db.GetUser(sUser.Email, sUser.Password);
            }

            ViewBag.Result = result == "Success" ? 1 : 2;
            ViewBag.User = Session["User"];
            return View("UserProfile");
        }

        public ActionResult UpdatePassword(User user, string oldPassword)
        {
            if (user == null || Session["User"] == null)
                return RedirectToAction("Index", "Home");

            string hashOldPass = PublicFunction.HashPassword(oldPassword);
            oldPassword = PublicFunction.HashPassword(hashOldPass);
            User sUser = (User)Session["User"];
            if (oldPassword != sUser.Password)
                return RedirectToAction("UserProfile");

            string hashNewPass = PublicFunction.HashPassword(user.Password);
            user.Password = PublicFunction.HashPassword(hashNewPass);

            string result = db.UpdateUserPassword(sUser.ID, user.Password);
            if (result == "Success")
                Session["User"] = db.GetUser(sUser.Email, user.Password);

            ViewBag.User = Session["User"];
            return View("UserProfile");
        }

        public ActionResult UpdateAddress(string newAddress, string newPostal)
        {
            string result;
            if (Session["User"] == null)
                return RedirectToAction("Index", "Home");

            if (string.IsNullOrEmpty(newAddress) && string.IsNullOrEmpty(newPostal))
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            User user = (User) Session["User"];
            if (!string.IsNullOrEmpty(newAddress))
                result = db.UpdateAddress(user.ID, newAddress);
            if (!string.IsNullOrEmpty(newPostal))
            {
                result = db.UpdatePostal(user.ID, newPostal);
            }
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}