using MIPChat.DAL.UnitOfWork;
using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MIPChat.Controllers
{

    public class AccountController : Controller
    {
        private ChatUnitOfWork ChatDatafWorker { get; set; }

        public AccountController()
        {
            ChatDatafWorker = new ChatUnitOfWork();
        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(RegisterModel model)
        {
            User user = null;

            user = ChatDatafWorker.Users.FindAll().Result.FirstOrDefault(u => u.Name == model.Name);


            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.Name, true);
                return RedirectToAction("Index", "Messanger");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is incorrect!");
            }
            return View();
        }

        // GET: Account
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterModel model)
        {
            User user = null;

            user = ChatDatafWorker.Users.FindAll().Result.FirstOrDefault(u => u.Name == model.Name);

            if (user == null)
            {

                ChatDatafWorker.Users.Insert(new User { Name = model.Name, Password = model.Password, Email = model.Email });
                ChatDatafWorker.CommitChanges();


                user = ChatDatafWorker.Users.FindAll().Result.Where(u => u.Name == model.Name && u.Password == model.Password).FirstOrDefault();
            }
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.Name, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Username occupied!");
            }
            return View();
        }


    }
}
    