using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIPChat.Helpers
{
    public static class Mapper
    {
        public static UserViewModel ToView(User User)
        {
            return new UserViewModel
            {
                UserId = User.UserId,
                Email = User.Email,
                Icon = User.Icon,
                Name = User.Name,
                Surname = User.Surname
            };
        }
        public static User ToDomain(UserViewModel User)
        {
            return new User
            {
                UserId = User.UserId,
                Email = User.Email,
                Icon = User.Icon,
                Name = User.Name,
                Surname = User.Surname
            };
        }
        public static User ToDomain(RegisterModel User)
        {
            return new User
            {
                Email = User.Email,
                Icon = User.Icon,
                Name = User.Name,
                Surname = User.Surname
            };
        }

        public static User ToDomain(LoginModel User)
        {
            return new User
            {
                Email = User.Email
            };
        }
        
    }
}