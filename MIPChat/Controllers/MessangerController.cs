using MIPChat.DAL;
using MIPChat.DAL.Repository;
using MIPChat.DAL.UnitOfWork;
using MIPChat.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MIPChat.Controllers
{

    [Authorize]
    public class MessangerController : Controller
    {

        private ChatUnitOfWork messangerData;
        //private ChatDBContext db;

        public MessangerController()
        {
            messangerData = new ChatUnitOfWork();
            //db = new ChatDBContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            // /*Data stub*/
            // ViewBag.Chats = messangerData.Chats.FindAll();
            // ViewBag.Messages = messangerData.Messages.FindAll();
            // ViewBag.Users = messangerData.Users.FindAll();
            // ViewBag.CurrentUser = messangerData.Chats.FindAll().First();
            return View();
        }

        [HttpPost]
        public ActionResult GetAllChatsForUser(Guid userId)
        {

            var chats = messangerData.Chats.FindAll().ToList();

            //var chats1 = messangerData.Users.FindById(userId).Chats;

            return PartialView(chats);
        }



        [HttpPost]
        public ActionResult FindChat(Guid ChatID)
        {
            var chatInst = messangerData.Chats.FindById(ChatID).Messages;

            return PartialView(chatInst);
        }



        [HttpPost]
        public ActionResult GetAllUsersToChat()
        {
            var userList = messangerData.Users.FindAll();
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
                    messangerData.Chats.Insert(new ChatModel() { Name = name, Users = new List<User>(users), ChatId = Guid.NewGuid(), IsLocal = true });
                }
                else if (users.Count > 2)
                {
                    //create chat
                    messangerData.Chats.Insert(new ChatModel() { Name = name, ChatId = Guid.NewGuid(), IsLocal = false, Users = users });

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

            correntChat.Messages.Add(new Message() { Content = message, TheTimeOfSending = DateTime.Now, Chat = correntChat, Author = correntUser, MessageId = Guid.NewGuid() });
            messangerData.CommitChanges();
            return PartialView();
        }
    }
}