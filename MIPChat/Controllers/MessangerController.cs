using MIPChat.DAL;
using MIPChat.DAL.Repository;
using MIPChat.DAL.UnitOfWork;
using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MIPChat.Controllers
{
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
            /*Data stub*/
            ViewBag.Chats = messangerData.Chats.FindAll().Result;
            ViewBag.Messages = messangerData.Messages.FindAll().Result;
            ViewBag.Users = messangerData.Users.FindAll().Result;
            ViewBag.CurrentUser = messangerData.Chats.FindAll().Result.First();
            return View();
        }

        [HttpPost]
        public ActionResult GetAllChatsForUser(Guid userId)
        {
            var allExistingChatsForUser = messangerData.Users.FindById(userId).Result.Chats;

            return PartialView(allExistingChatsForUser);
        }



        [HttpPost]
        public ActionResult GetChats(Guid ChatID)
        {
            var chatInst = messangerData.Chats.FindById(ChatID).Result;

            return PartialView(chatInst);
        }



        [HttpPost]
        public ActionResult GetAllUsersToChat(Guid UserId)
        {
            var userList = messangerData.Users.FindAll().Result;

            return PartialView(userList);
        }


        [HttpPost]
        public ActionResult GetAllUsersToMessage(Guid userId)
        {
            dynamic existingMessagesForUser = messangerData.Users.FindById(userId).Result.Chats.Where(u => u.Users.Count == 2);

            ICollection<User> userList = new List<User>();

            foreach (var user in messangerData.Users.FindAll().Result)
            {
                if (!existingMessagesForUser.Contains(user))
                {
                    userList.Add(user);
                }
            }

            return PartialView(userList);
        }







        [HttpPost]
        public ActionResult CreateMessageOrChat(string name, ICollection<User> users, byte[] icon = null)
        {
            if (users.Count == 2)
            {
                //create messagechat
                messangerData.Chats.Insert(new ChatModel() { Icon = icon, Name = name, Users = users });
            }
            else if (users.Count > 2)
            {
                //create chat
                messangerData.Chats.Insert(new ChatModel() { Icon = icon, Name = name, Users = users });

            }
            //132
            return PartialView();
        }
        [HttpPost]
        public ActionResult SendMessage(string message, Guid chatId, Guid UserSenderId)
        {
            messangerData.Chats.FindById(chatId).Result.Messages.Add(new Message() { Content = message, AuthorId = UserSenderId, TheTimeOfSending = DateTime.Now });

            return PartialView();
        }
    }
}