using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIPChat.Helpers
{
    public static class Mapper
    {
        public static User Map(UserViewModel User)
        {
            return new User
            {
                UserId = User.UserId,
                Email = User.Email,
                Icon = User.Icon,
                Login = User.Login,
                Name = User.Name,
                LastLogOut = User.LastLogOut,
                Surname = User.Name
            };
        }

        public static UserViewModel Map(User User)
        {
            return new UserViewModel
            {
                UserId = User.UserId,
                Email = User.Email,
                Icon = User.Icon,
                Login = User.Login,
                Name = User.Name,
                LastLogOut = User.LastLogOut,
                Surname = User.Name
            };
        }
    }
}