using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Portfolio.Models.Authorization
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Name);
        }
    }

    [EntityTypeConfiguration(typeof(GroupConfiguration))]
    public class Group
    {
        public string Name { get; set; } = string.Empty;
        public Role[]? Roles { get; set; } = null!;
    }
}
