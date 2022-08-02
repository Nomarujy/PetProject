using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Portfolio.Models.Authorization
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Name);
        }
    }

    [EntityTypeConfiguration(typeof(RoleConfiguration))]
    public class Role
    {
        public int Id { get; set; }

        public string? GroupName { get; set; }
        public Group? Group { get; set; }

        public string Name { get; set; } = null!;
        public Permision[]? Permisions { get; set; }

        public static Role GetDefaultRole()
        {
            return new Role { Id = 1, Name="User" };
        }
    }
}
