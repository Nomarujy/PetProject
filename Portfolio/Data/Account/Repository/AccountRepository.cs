using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Context;
using Portfolio.Models.Authorization;
using System.Security.Claims;

namespace Portfolio.Data.Account.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext database;

        public AccountRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public void AddUser(User user)
        {
            database.Users.Add(user);
            database.SaveChanges();
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
            return database.Permisions.Where(p => p.RoleId == RoleId).ToArray();
        }

        public ClaimsPrincipal GetClaimPrincipals(User user)
        {
            Permision?[] permisions = GetPermisionse(user.RoleId);

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            if (user.Role.GroupName != null)
                Claims.Add(new Claim("Group", user.Role.GroupName!));

            if (permisions != null)
            {
                foreach (var permision in permisions)
                {
                    Claims.Add(new Claim(permision!.Category, permision.GetCRUD()));
                }
            }

            var Identities = new ClaimsIdentity(Claims, "Cookies");
            return new ClaimsPrincipal(Identities);
        }
    }
}
