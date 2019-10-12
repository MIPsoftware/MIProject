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
            DateTime lastLogOut = User.LastLogOut;
            DateTime lastLogIn = User.LastLogIn;
            String OnlineStatus;

            if(lastLogIn.Millisecond > lastLogOut.Millisecond)
            {
                OnlineStatus = "Online";
            }
            else if(lastLogOut.Date == DateTime.Today)
            {
                OnlineStatus = lastLogOut.ToString("HH:mm tt");
            }else
            {
                OnlineStatus = lastLogOut.ToString("MM.dd.yyyy hh:mm tt");
            }

            return new UserViewModel
            {
                UserId = User.UserId,
                Email = User.Email,
                Icon = User.Icon,
                Name = User.Name,
                Surname = User.Surname,
                OnlineStatus = OnlineStatus
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

        public static MessageModel ToView(Message message)
        {

            return new MessageModel
            {
                Id = message.Id,
                AuthorId = message.AuthorId,
                Content = message.Content,
                TheTimeOfSending = message.TheTimeOfSending.ToString("HH:mm tt")
            };
        }
        
    }
}