using System.ComponentModel.DataAnnotations;

namespace MIPChat.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Обязательное поле")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Password { get; set; }
    }
}
