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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserMessAndChatsAsync(User user)
        {
            var chatRepository = new ChatRepository(new ChatDBContext());
            var allChats = chatRepository.FindAllChatsForUser(user).Result;

            return PartialView(allChats);
        }

        [HttpPost]
        public ActionResult AvailNewMessAndChats(Guid userID)
        {
            var userRepository = new UserRepository(new ChatDBContext())
            {

            }



            return PartialView();
        }


        [HttpPost]
        public ActionResult UserConcreteChat(int ChatID)
        {

            return PartialView();
        }


    }
}