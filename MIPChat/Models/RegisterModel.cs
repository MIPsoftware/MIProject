using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIPChat.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Surname { get; set; }

        public byte[] Icon { get; set; } 
    }
}