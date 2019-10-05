using MIPChat.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIPChat.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime LastLogOut { get; set; }
        public byte[] Icon { get; set; }
    }
}