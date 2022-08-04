using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.Account.Encryptor;

namespace Portfolio.Models.Authorization
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Email);
        }
    }

    [EntityTypeConfiguration(typeof(UserConfiguration))]
    public class User
    {
        public User() { }

        public User(RegisterForm form, IPasswordEncryptor encryptor)
        {
            Email = form.Email;
            Password = encryptor.EncryptPassword(form.Password);

            Created = DateTime.UtcNow;

            RoleId = Role.GetDefaultUser().Id;

            Username = form.Username;
        }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public DateTime Created { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public string Username { get; set; } = null!;

        public bool Deleted { get; set; } = false;
        public bool Baned { get; set; } = false;
    }
}
