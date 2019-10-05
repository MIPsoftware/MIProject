using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MIPChat.DAL.Domain;

namespace MIPChat.Models
{
    public class User
    {   [Key]
        public Guid UserId { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="У пользователя должен быть Email")]
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime LastLogIn { get; set; }
        public DateTime LastLogOut { get; set; }
        public byte[] Icon { get; set; }
    }
}