using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GroupChat.Models;

namespace GroupChat.Controllers
{

    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.Groups = new List<Group>()
                {
                    new Group() { GroupId = 1, Name = "BestGroupEver", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg" },
                    new Group() { GroupId = 2, Name = "foooooooooooo", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg" },
                    new Group() { GroupId = 3, Name = "bar", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg" },
                    new Group() { GroupId = 4, Name = "new group here", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg" },
                };
            ViewBag.Messages = new List<Message>()
                {
                    new Message() { UserId = 1, Text = "I am banan", Avatar = "https://sun9-52.userapi.com/c851336/v851336737/1a4a49/6zOXw4lM38E.jpg", Time = "8:40 AM, Today" },
                    new Message() { UserId = 2, Text = "You are banan", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Time = "8:55 AM, Today" },
                    new Message() { UserId = 1, Text = "Oy shit", Avatar = "https://sun9-52.userapi.com/c851336/v851336737/1a4a49/6zOXw4lM38E.jpg", Time = "9:00 AM, Today" },
                    new Message() { UserId = 2, Text = "Here we go again", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Time = "9:05 AM, Today" },
                };
            ViewBag.Users = new List<User>()
                {
                    new User() { UserId = 1, Name = "somebody", SelectedGroup = "bar", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Status = "Online" },
                    new User() { UserId = 2, Name = "petyx", SelectedGroup = "bar", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Status = "Online" },
                    new User() { UserId = 3, Name = "inokenti", SelectedGroup = "bar", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Status = "Online" },
                    new User() { UserId = 4, Name = "erzan", SelectedGroup = "bar", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Status = "Online" },
                    new User() { UserId = 5, Name = "kekol", SelectedGroup = "bar", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Status = "Online" },
                    new User() { UserId = 6, Name = "lolek", SelectedGroup = "bar", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Status = "Online" },
                    new User() { UserId = 7, Name = "bolek", SelectedGroup = "bar", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Status = "Online" },
                    new User() { UserId = 8, Name = "qwerty", SelectedGroup = "bar", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Status = "Online" },
                };
            ViewBag.CurrentUser = new User() { UserId = 1, Name = "somebody", SelectedGroup = "bar", Avatar = "https://sun9-18.userapi.com/c845522/v845522717/546d0/HsouPDmDQHo.jpg", Status = "Online" };
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
