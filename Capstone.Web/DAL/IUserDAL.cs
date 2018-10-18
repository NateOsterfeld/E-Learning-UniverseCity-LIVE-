using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IUserDAL
    {
        List<string> GetUsernames(string startsWith);
        List<User> GetUsers();
        bool ChangePassword(string username, string newPassword);
        bool CreateUser(User newUser);
    }
}