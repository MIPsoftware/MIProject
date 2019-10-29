namespace MIPChat.Migrations
{
    using MIPChat.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MIPChat.DAL.ChatDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MIPChat.DAL.ChatDBContext";
        }

        protected override void Seed(MIPChat.DAL.ChatDBContext context)
        {

            User user = new User
            {
                UserId = Guid.NewGuid(),
                Email = "Prok@gmail.com",
                LastLogIn = new DateTime(2006, 12, 2, 4, 0, 0),
                LastLogOut = new DateTime(2006, 12, 2, 12, 0, 0),
                Name = "Kek",
                Surname = "tok",
                Password = "2131",
            };

            ChatModel chat = new ChatModel
            {
                ChatId = Guid.NewGuid(),
                Name = "POAS",
                Users = { user }
            };

     

            Message message = new Message
            {
                MessageId = Guid.NewGuid(),
                Content = "DIdasmdas",
                TheTimeOfSending = DateTime.Now,
                Author = user,
                Chat = chat
            };

          

            context.Users.Add(user);
            context.Chats.Add(chat);
            context.Messages.Add(message);

            user.Chats.Add(chat);
            context.Users.AddOrUpdate(user);

            chat.Messages.Add(message);
            context.Chats.AddOrUpdate(chat);
        }
    }
}
