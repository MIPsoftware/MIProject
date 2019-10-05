using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIPChat.Helpers
{
    public static class Mapper
    {
        public static UserViewModel Map(User User)
        {
            return new UserViewModel
            {
                Icon = User.Icon,
                Name = User.Name,
                LastLogOut = User.LastLogOut,
                Surname = User.Name
            };
        }

        public static User Map(LoginModel User)
        {
            return new User
            {
                Email = User.Email
            };
        }
    }
}