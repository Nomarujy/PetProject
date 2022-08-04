using Portfolio.Models.Authorization;
using System.Security.Claims;

namespace Portfolio.Data.Account.Repository
{
    public interface IAccountRepository
    {
        public User? GetUser(string Email);

        public User? GetUserWithRole(string Email);

        public void AddUser(User user);
        public Permision?[] GetPermisionse(int RoleId);

        public ClaimsPrincipal GetClaimPrincipals(User user);
    }
}
