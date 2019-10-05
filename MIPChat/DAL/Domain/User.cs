using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIPChat.DAL.Domain;

namespace MIPChat.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime LastLogIn { get; set; }
        public DateTime LastLogOut { get; set; }
        public byte[] Icon { get; set; }
    }
}