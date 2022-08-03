using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Models.Extension;
using Portfolio.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class CotactConfigurator : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(c => c.Id).HasName("PK_Id");
        }
    }

    [EntityTypeConfiguration(typeof(CotactConfigurator))]
    public class Contact : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Message { get; set; } = "";


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new();

            //TO:DO Better email validation
            if (EmailValidation.EmailIsValid(Email) == false) result.AddToResult("Email not valid", "Email");

            return result;
        }

    }
}
