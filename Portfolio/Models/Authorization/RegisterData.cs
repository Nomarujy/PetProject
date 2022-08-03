using Portfolio.Models.Extension;
using Portfolio.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Authorization
{
    public class RegisterForm : IValidatableObject
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RepeatPassword { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new();

            if (EmailValidation.EmailIsValid(Email) == false) result.AddToResult("Email not valid", "Email");

            if (Username.Contains("admin")) result.AddToResult("Username contain \"Admin\"", "Username");

            if (Password.Length < 6) result.AddToResult("Password is too short", "Password");
            if (Password.Equals(RepeatPassword) == false) result.AddToResult("Passwords not equal", "Password", "RepeatPassword");

            return result;
        }
    }
}
