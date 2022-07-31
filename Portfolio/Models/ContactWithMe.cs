using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.Extension.Validation;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class CotactWithMeConfigurator : IEntityTypeConfiguration<ContactWithMe>
    {
        public void Configure(EntityTypeBuilder<ContactWithMe> builder)
        {
            builder.HasKey(c => c.Id).HasName("PK_Id");
        }
    }

    [EntityTypeConfiguration(typeof(CotactWithMeConfigurator))]
    public class ContactWithMe : IValidatableObject
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
            if (Email.Contains('@') == false) result.AddToResult("Email haven`t @", "Email");

            return result;
        }

    }
}
