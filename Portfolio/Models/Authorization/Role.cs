using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Portfolio.Models.Authorization
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(c => c.Id).HasName("PK_Id");
            builder.HasAlternateKey(c => c.GroupId).HasName("FK_GroupName");
            builder.HasIndex(c => c.Name);
        }
    }

    public class Role
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }
        public Group? Group { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string[]? Permisions { get; set; } = null!;
    }
}
