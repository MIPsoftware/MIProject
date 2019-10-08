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

        public MessangerController()
        {
            messangerData = new ChatUnitOfWork(new ChatDBContext());
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserMessAndChatsAsync(User user)
        {
            var allExistingChats = messangerData.Chats.FindAllChatsForUser(user).Result;

            return PartialView(allExistingChats);
        }

        [HttpPost]
        public ActionResult AvailNewMesssges(User user)
        {
           
            var NewExChats = messangerData.Chats.FindAllChatsForUser(user).Result.ToList();

            //var userRepository = new UserRepository(new ChatDBContext());
            //var allAvailableUsers = userRepository.FindAllAvailable().Result;


            return PartialView();//allAvailableUsers);
        }






        [HttpPost]
        public ActionResult UserConcreteChat(Guid ChatID)
        {
            var chatInst = messangerData.Chats.FindById(ChatID).Result;

            return PartialView(ChatID);
        }


        [HttpPost]
        public ActionResult FindChat(string name)
        {
            ChatModel chatModel = new ChatModel();// = chatRepository.FindByName(name);

            return PartialView(chatModel);
        }



    }
}