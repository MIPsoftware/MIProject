using MIPChat.DAL;
using MIPChat.DAL.UnitOfWork;
using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MIPChat.Controllers
{


    public class AccountController : Controller
    {
        private readonly ChatUnitOfWork uof;


        public AccountController()
        {
            uof = new ChatUnitOfWork();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
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

            user = uof.Users.FindAll().FirstOrDefault(u => u.Name == model.Name);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.Name, true);

                HttpCookie authCookie = new HttpCookie("AuthCookie");
                authCookie.Values.Add("UserName", model.Name);
                Response.Cookies.Add(authCookie);

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
            user = uof.Users.FindAll().FirstOrDefault(u => u.Name == model.Name);

            if (user == null)
            {

                uof.Users.Insert(new User { UserId = Guid.NewGuid(), Name = model.Name, Password = model.Password, Email = model.Email, LastLogIn = DateTime.Now, LastLogOut = DateTime.Now, Surname = model.Surname });
                uof.CommitChanges();
                user = uof.Users.FindAll().Where(u => u.Name == model.Name && u.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);

                    HttpCookie authCookie = new HttpCookie("AuthCookie");
                    authCookie.Values.Add("UserName", model.Name);
                    Response.Cookies.Add(authCookie);

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Username occupied!");
            }
            return View();
        }
    }
}
