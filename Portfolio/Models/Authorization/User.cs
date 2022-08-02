using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Portfolio.Models.Authorization
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasAlternateKey(c => c.RoleId);
            builder.HasIndex(c => c.Email);
        }
    }

    [EntityTypeConfiguration(typeof(UserConfiguration))]
    public class User
    {
        public User() { }

        public User(RegisterForm form)
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;

            Role = Role.GetDefaultRole();

            Username = form.Username;
            Email = form.Email;
            Password = form.Password;
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public bool Deleted { get; set; } = false;
        public bool Baned { get; set; } = false;
    }
}
