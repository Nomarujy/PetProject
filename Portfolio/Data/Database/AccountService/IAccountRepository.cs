using Portfolio.Models.Authorization;

namespace Portfolio.Data.Database.AccountService
{
    public interface IAccountRepository
    {
        public User? GetUser(string Email);

        public User? GetUserWithRole(string Email);

        public void AddUser(User user);
        public Permision?[] GetPermisionse(int RoleId);
    }
}
