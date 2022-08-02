using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Portfolio.Models.Authorization
{
    partial class PermisionConfiguration : IEntityTypeConfiguration<Permision>
    {
        public void Configure(EntityTypeBuilder<Permision> builder)
        {
            builder.HasKey(p => p.Category);
        }
    }

    [EntityTypeConfiguration(typeof(PermisionConfiguration))]
    public class Permision
    {
        public string Category { get; set; } = null!;

        public bool Create { get; set; } = false;
        public bool Read { get; set; } = false;
        public bool Update { get; set; } = false;
        public bool Delete { get; set; } = false;

        public string GetCRUD()
        {
            var sb = new StringBuilder();
            if (Create) sb.Append('C');
            if (Read) sb.Append('R');
            if (Update) sb.Append('U');
            if (Delete) sb.Append('D');

            return sb.ToString();
        }
    }
}
