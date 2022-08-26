using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Authentication.FormModel
{
    public class RegisterForm
    {
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; } = string.Empty;

        [DisplayName("Почта"), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password), Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DisplayName("Подвердите пароль")]
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
