using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Database.Context;
using Portfolio.Models.Authorization;

namespace Portfolio.Data.Database.AccountService
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext database;

        public AccountRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public User? GetUser(string Email)
        {
            return database.Users.FirstOrDefault(u => u.Email == Email);
        }

        public User? GetUserWithRole(string Email)
        {
            return database.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == Email);
        }

        public Permision?[] GetPermisionse(int RoleId)
        {
            return database.Permisions.Where(p=> p.RoleId == RoleId).ToArray();
        }

        public void AddUser(User user)
        {
            database.Users.Add(user);
            database.SaveChanges();
        }
    }
}
