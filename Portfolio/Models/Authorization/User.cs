using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Portfolio.Models.Extension;
using Portfolio.Models.Validation;

namespace Portfolio.Models.Authorization
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id).HasName("PK_ID");
            builder.HasAlternateKey(c => c.RoleName).HasName("FK_RoleName");
            builder.HasIndex(c => c.Email);
        }
    }

    public class User : IValidatableObject
    {
        public User()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;

            Deleted = false;
            Baned = false;
        }

        [BindNever]
        public Guid Id { get; set; }

        [BindNever]
        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [BindNever]
        public DateTime Created { get; set; }

        [BindNever]
        public bool Deleted { get; set; }
        [BindNever]
        public bool Baned { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new();
            if (EmailValidation.EmailIsValid(Email) == false) result.AddToResult("Email not valid", "Email");
            if (Password.Length < 6) result.AddToResult("Password is too short", "Password");

            return result;
        }
    }
}
