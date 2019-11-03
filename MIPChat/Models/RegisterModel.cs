using System.ComponentModel.DataAnnotations;

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