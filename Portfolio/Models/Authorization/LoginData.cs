using Portfolio.Models.Extension;
using Portfolio.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Authorization
{
    public class LoginForm : IValidatableObject
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new();
            if (EmailValidation.EmailIsValid(Email) == false) result.AddToResult("Email not valid", "Email");
            if (Password.Length < 6) result.AddToResult("Password is too short", "Password");

            return result;
        }
    }
}
