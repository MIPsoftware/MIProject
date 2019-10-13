using MIPChat.DAL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIPChat.Models
{
    public class UserViewModel
    {
        [Key]
        public Guid UserId { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "У пользователя должен быть Email")]
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] Icon { get; set; }
        
        public string OnlineStatus { get; set; }
    }
}
