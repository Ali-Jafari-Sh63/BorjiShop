using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BorjiShop.Models;

namespace BorjiShop.DAL
{
    public class DAL_User
    {
        private BorjiShopContext db = new BorjiShopContext();

        public string Login(User user)
        {
            string result;
            try
            {
                var existUser = GetUser(user.Email, user.Password);
                result = existUser == null ? "پست الکترونیک یا رمز عبور اشتباه  است" : "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return result;
        }

        public User GetUser(string emial, string password)
        {
            var result = db.Users.FirstOrDefault(u => u.Email == emial && u.Password == password);
            return result;
        }

        public User GetUser(string activationCode)
        {
            var result = db.Users.FirstOrDefault(u => u.ActivationCode == activationCode);
            return result;
        }

        public User GetUser(int id)
        {
            var result = db.Users.FirstOrDefault(u => u.ID == id);
            return result;
        }

        public bool IsAdmin(User user)
        {
            return Convert.ToBoolean(GetUser(user.Email, user.Password).UserRole);
        }

        public bool IsActiveUser(User user)
        {
            return Convert.ToBoolean(GetUser(user.Email, user.Password).IsActive);
        }

        public bool IsDeletedUser(User user)
        {
            return Convert.ToBoolean(GetUser(user.Email, user.Password).IsDeleted);
        }

        public string AddUser(User user)
        {
            string result;
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public bool ExistUser(User user)
        {
            User existUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existUser == null)
                return false;
            return true;
        }

        public void AddRememberMe(User user, string rememberMe)
        {
            var u = GetUser(user.Email, user.Password);
            u.RememberMe = rememberMe;
            db.SaveChanges();
        }

        public User ExistRememberMe(string rememberMe)
        {
            var user = db.Users.FirstOrDefault(u => u.RememberMe == rememberMe);
            return user;
        }

        public string ActiveUser(User user)
        {
            var result = string.Empty;

            try
            {
                user.IsActive = true;
                user.ActivationCode = Guid.NewGuid().ToString().Replace("-", "");
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public string UpdateUser(int id, string firstName, string lastName, string phone)
        {
            string result;
            try
            {
                var user = GetUser(id);
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Phone = phone;
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }

        public string UpdateUserPassword(int id, string newPassword)
        {
            string result;
            try
            {
                var user = GetUser(id);
                user.Password = newPassword;
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return result;
        }

        public string UpdateAddress(int id, string newAddress)
        {
            string result;
            try
            {
                var user = GetUser(id);
                user.Address = newAddress;
                db.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return result;
        }

        public string UpdatePostal(int id, string newPostal)
        {
            string result;
            try
            {
                var user = GetUser(id);
                user.PostalCode = newPostal;
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