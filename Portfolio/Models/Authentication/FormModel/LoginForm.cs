using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Portfolio.Models.Authentication.FormModel
{
    public class LoginForm
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; } = false;

        public string ReturnUrl { get; set; } = "/";
    }
}
