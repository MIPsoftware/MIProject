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
            var allExistingChatsForUser = messangerData.Users.FindById(userId).Chats;

            return PartialView(allExistingChatsForUser);
        }



        [HttpPost]
        public ActionResult FindChat(Guid ChatID)
        {
            var chatInst = messangerData.Chats.FindById(ChatID);

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

            var users = new List<User>();
            foreach(var guid in usersGuids)
            {
                users.Add(messangerData.Users.FindById(guid));
            }




            if (users.Count == 2)
            {
                //create messagechat
                messangerData.Chats.Insert(new ChatModel() {Name = name, Users = users });
            }
            else if (users.Count > 2)
            {
                //create chat
                messangerData.Chats.Insert(new ChatModel() {Name = name, Users = users, ChatId = Guid.NewGuid() });

            }
            //132
            return PartialView();
        }
        [HttpPost]
        public ActionResult SendMessage(string message, Guid chatId, Guid UserSenderId)
        {
            messangerData.Chats.FindById(chatId).Messages.Add(new Message() { Content = message, AuthorId = UserSenderId, TheTimeOfSending = DateTime.Now });

            return PartialView();
        }
    }
}