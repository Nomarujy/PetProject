using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Models.Validation;
using Portfolio.Utilites.Extension;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Contact
{
    public class CotactConfigurator : IEntityTypeConfiguration<ContactModel>
    {
        public void Configure(EntityTypeBuilder<ContactModel> builder)
        {
            builder.HasKey(c => c.Id).HasName("PK_Id");
        }
    }

    [EntityTypeConfiguration(typeof(CotactConfigurator))]
    public class ContactModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Message { get; set; } = "";


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new();

            if (EmailValidation.EmailIsValid(Email) == false) result.AddToResult("Email not valid", "Email");

            return result;
        }

    }
}
