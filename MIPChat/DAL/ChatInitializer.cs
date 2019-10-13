using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MIPChat.DAL
{
    class ChatInitializer : DropCreateDatabaseAlways<ChatDBContext>
    {
        protected override void Seed(ChatDBContext db)
        {
            /*Change Email to Icon*/
            /*LastLogIn and LastLogOut => Status as string: online or offline*/
            User u1 = new User() { UserId = Guid.NewGuid(), Email = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Name = "somebody", Surname = "ones told me", LastLogIn = DateTime.Now, LastLogOut = DateTime.Now, Icon = new byte[0] };
            User u2 = new User() { UserId = Guid.NewGuid(), Email = "https://img2.akspic.ru/image/114740-apelsin-ogon-pozhar-plamya-grafika-2880x1800.jpg", Name = "petyx", Surname = "petyshok", LastLogIn = DateTime.Now, LastLogOut = DateTime.Now, Icon = new byte[0] };
            User u3 = new User() { UserId = Guid.NewGuid(), Email = "https://images.wallpaperscraft.ru/image/ogon_plamya_koster_temnyj_fon_119463_2048x1152.jpg", Name = "inokenti", Surname = "sergio", LastLogIn = DateTime.Now, LastLogOut = DateTime.Now, Icon = new byte[0] };
            User u4 = new User() { UserId = Guid.NewGuid(), Email = "https://www.patriotheadquarters.com/wp-content/uploads/2019/05/fire-1260723_1920.jpg", Name = "erzan", Surname = "Vstavai", LastLogIn = DateTime.Now, LastLogOut = DateTime.Now, Icon = new byte[0] };
            db.Users.AddRange(new List<User>() { u1, u2, u3, u4 });
            /*TheTimeOfSending => format: "9:00 AM, Today"*/
            Message m1 = new Message() { MessageId = 1, Content = "I am banan", AuthorId = u1.UserId, TheTimeOfSending = DateTime.Now };
            Message m2 = new Message() { MessageId = 2, Content = "You are banan", AuthorId = u2.UserId, TheTimeOfSending = DateTime.Now };
            Message m3 = new Message() { MessageId = 3, Content = "Oy shit", AuthorId = u1.UserId, TheTimeOfSending = DateTime.Now };
            Message m4 = new Message() { MessageId = 4, Content = "Here we go again", AuthorId = u2.UserId, TheTimeOfSending = DateTime.Now };
            db.Messages.AddRange(new List<Message> { m1, m2, m3, m4 });
            /*Images for chat*/
            ChatModel cm1 = new ChatModel() { ChatId = 1, Name = "About life", Users = new List<User>() { u1, u2 }, Messages = new List<Message>() { m1, m2, m3, m4 } };
            ChatModel cm2 = new ChatModel() { ChatId = 2, Name = "Empty", Users = new List<User>() { u1, u3, u4 }, Messages = new List<Message>() };
            db.Chats.AddRange(new List<ChatModel> { cm1, cm2 });
            db.SaveChanges();
        }
    }
}