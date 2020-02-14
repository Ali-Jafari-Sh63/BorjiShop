using System;
using System.Collections.Generic;
using BorjiShop.DAL;
using BorjiShop.Models;

namespace BorjiShop.BLL
{
    public class BLL_User
    {
        private DAL_User dal_User = new DAL_User();

        public string Login(User user)
        {
            return dal_User.Login(user);
        }

        public bool IsAdmin(User user)
        {
            return dal_User.IsAdmin(user);
        }

        public bool IsActiveUser(User user)
        {
            return dal_User.IsActiveUser(user);
        }

        public bool IsDeletedUser(User user)
        {
            return dal_User.IsDeletedUser(user);
        }

        public User GetUser(string emial, string password)
        {
            var result = dal_User.GetUser(emial, password);
            return result;
        }

        public User GetUser(string activationCode)
        {
            return dal_User.GetUser(activationCode);
        }

        public User GetUser(int id)
        {
            return dal_User.GetUser(id);
        }

        public string AddUser(User user)
        {
            return dal_User.AddUser(user);
        }

        public bool ExistUser(User user)
        {
            return dal_User.ExistUser(user);
        }

        public void AddRememberMe(User user, string rememberMe)
        {
            dal_User.AddRememberMe(user, rememberMe);
        }

        public User ExistRememberMe(string rememberMe)
        {
            return dal_User.ExistRememberMe(rememberMe);
        }

        public string ActiveUser(User user)
        {
            return dal_User.ActiveUser(user);
        }

        public string UpdateUser(int id, string firstName, string lastName, string phone)
        {
            return dal_User.UpdateUser(id, firstName, lastName, phone);
        }

        public string UpdateUserPassword(int id, string newPassword)
        {
            return dal_User.UpdateUserPassword(id, newPassword);
        }

        public string UpdateAddress(int id, string newAddress)
        {
            return dal_User.UpdateAddress(id, newAddress);
        }

        public string UpdatePostal(int id, string newPostal)
        {
            return dal_User.UpdatePostal(id, newPostal);
        }


    }
}