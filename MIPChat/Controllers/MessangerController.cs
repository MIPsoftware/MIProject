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
        private ChatDBContext db;

        public MessangerController()
        {
            messangerData = new ChatUnitOfWork();
            db = new ChatDBContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            /*Data stub*/
            ViewBag.Chats = db.Chats.AsEnumerable();
            ViewBag.Messages = db.Messages.AsEnumerable();
            ViewBag.Users = db.Users.AsEnumerable();
            ViewBag.CurrentUser = db.Users.First();
            return View();
        }

        [HttpPost]
        public ActionResult GetAllChatsForUser(Guid userId)
        {
            var allExistingChatsForUser = messangerData.Users.FindById(userId).Result.Chats;

            return PartialView(allExistingChatsForUser);
        }

        [HttpPost]
        public ActionResult GetAllMessagesForUser(Guid userId)
        {
            var allExistingMessages = messangerData.Users.FindById(userId).Result.ChatMessages;

            return PartialView(allExistingMessages);
        }





        [HttpPost]
        public ActionResult GetChat(Guid ChatID)
        {
            var chatInst = messangerData.Chats.FindById(ChatID).Result;

            return PartialView(chatInst);
        }



        [HttpPost]
        public ActionResult GetMessage(Guid MessageID)
        {
            var msgInst = messangerData.Messages.FindById(MessageID).Result;

            return PartialView(msgInst);
        }


        [HttpPost]
        public ActionResult GetAllUsersToChat(Guid UserId)
        {
            var userList = messangerData.Users.FindAll().Result;

            return PartialView(userList);
        }


        [HttpPost]
        public ActionResult GetAllUsersToMessage(Guid UserId)
        {
            dynamic existingMessagesForUser = messangerData.Messages.FindAllToUser(UserId).Result();

            ICollection<UserViewModel> userList = new List<UserViewModel>();

            foreach(var user in messangerData.Users.FindAll().Result)
            {
                if(!existingMessagesForUser.Contains(user))
                {
                    userList.Add(user);
                }
            }

            var allusers = messangerData.Users.FindAll();


            return PartialView(userList);
        }
    }
}