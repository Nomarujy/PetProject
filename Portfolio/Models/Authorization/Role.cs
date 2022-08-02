using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Portfolio.Models.Authorization
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(c => c.Id).HasName("PK_Id");
            builder.HasAlternateKey(c => c.GroupName).HasName("FK_GroupName");
            builder.HasIndex(c => c.Name);
        }
    }

    public class Role
    {
        public int Id { get; set; }

        public string? GroupName { get; set; }
        public Group? Group { get; set; }

        public string Name { get; set; } = null!;
        public string[]? Permisions { get; set; }

        public static Role GetDefaultRole()
        {
            return new Role { Id = 1, Name="User" };
        }
    }
}
