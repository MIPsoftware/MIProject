using MIPChat.DAL.Domain;
using MIPChat.DAL.UnitOfWork;
using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MIPChat.Controllers
{

    [Authorize]
    public class MessangerController : Controller
    {

        private ChatUnitOfWork messangerData;

        public MessangerController()
        {
            messangerData = new ChatUnitOfWork();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAllChatsForUser(Guid userId)
        {
            var chats = messangerData.Chats.FindAll();
            return PartialView(chats);
        }



        [HttpPost]
        public ActionResult FindChat(Guid ChatID)
        {
            var chatInst = messangerData.Chats.FindById(ChatID).Messages;

            return PartialView(chatInst);

        }



        [HttpPost]
        public ActionResult GetAllUsersToChat(Guid correntUser)
        {
            var userList = messangerData.Users.FindAvailableUsersForChat(correntUser);
            return PartialView(userList);
        }


        [HttpPost]
        public ActionResult GetAllUsersToMessage(Guid userId)
        {
            dynamic existingMessagesForUser = messangerData.Users.FindById(userId).Chats.Where(u => u.Users.Count == 2);

            ICollection<User> userList = new List<User>();

            foreach (var user in messangerData.Users.FindAll())
            {
                if (!existingMessagesForUser.Contains(user))
                {
                    userList.Add(user);
                }
            }

            return PartialView(userList);
        }

        [HttpPost]
        public ActionResult CreateMessageOrChat(string name, ICollection<Guid> usersGuids)
        {
            if (usersGuids.Count >= 2)
            {
                var users = new List<User>();
                foreach (var guid in usersGuids)
                {
                    users.Add(messangerData.Users.FindById(guid));
                }

                if (users.Count == 2)
                {
                    //create messagechat
                    messangerData.Chats.Insert(new Chat() { Name = name, 
                                                                 Users = new List<User>(users), 
                                                                 ChatId = Guid.NewGuid(), 
                                                                 IsLocal = true
                                                                });
                }
                else if (users.Count > 2)
                {
                    //create chat
                    messangerData.Chats.Insert(new Chat() {  Name = name, 
                                                                  ChatId = Guid.NewGuid(), 
                                                                  IsLocal = false, 
                                                                  Users = users
                                                                });

                }
                messangerData.CommitChanges();
                //132
                return PartialView();
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult SendMessage(string message, Guid chatId, Guid UserSenderId)
        {
            var correntChat = messangerData.Chats.FindById(chatId);
            var correntUser = messangerData.Users.FindById(UserSenderId);

            correntChat.Messages.Add(new Message() { Content = message, 
                                                     TheTimeOfSending = DateTime.Now, 
                                                     ChatId = correntChat.ChatId, 
                                                     UserId = correntUser.UserId, 
                                                     MessageId = Guid.NewGuid() 
                                                   });
            messangerData.CommitChanges();
            return PartialView();
        }
    }
}