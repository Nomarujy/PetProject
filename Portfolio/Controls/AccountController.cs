using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Portfolio.Models.Authorization;

namespace Portfolio.Controls
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register(User user)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(User user)
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return LocalRedirect("/");
        }

        private void SignInUser(User user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Username)
            };

            if (user.Role != null)
            {
                Claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));

                if (user.Role.GroupId != null)
                    Claims.Add(new Claim(ClaimTypes.GroupSid, user.Role.GroupId.ToString()!));

                if (user.Role.Permisions != null)
                {
                    foreach (var permision in user.Role.Permisions)
                    {
                        Claims.Add(new Claim(permision, "Have"));
                    }
                }
            }

            var Identities = new ClaimsIdentity(Claims);
            var ClaimPrinc = new ClaimsPrincipal(Identities);
            HttpContext.SignInAsync(ClaimPrinc);
        }
    }
}
