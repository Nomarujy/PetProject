using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Portfolio.Models.Authorization;
using Portfolio.Data.Database.AccountService;
using Portfolio.Data.Authorization;

namespace Portfolio.Controls
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository database;
        private readonly IPasswordEncryptor passwordEncryptor;

        public AccountController(IAccountRepository database, IPasswordEncryptor passwordEncryptor)
        {
            this.database = database;
            this.passwordEncryptor = passwordEncryptor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return LocalRedirect("/");
        }

        [HttpPost]
        public IActionResult Register(RegisterForm userForm)
        {
            if (ModelState.IsValid == false) return BadRequest(ModelState.Values);

            User user = new(userForm);


            SignInUser(user);
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginForm userForm, string? returnUrl)
        {
            User? user = database.GetUser(userForm.Email);

            if (user != null) return BadRequest("UserNotFound");

            if (passwordEncryptor.PasswordEqual(userForm.Password, user!.Password) == false)
            {
                return BadRequest();
            }



            SignInUser(user!);
            return LocalRedirect(returnUrl??"/");
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

                if (user.Role.GroupName != null)
                    Claims.Add(new Claim("Group", user.Role.GroupName!));

                if (user.Role.Permisions != null)
                {
                    foreach (var permision in user.Role.Permisions)
                    {
                        Claims.Add(new Claim(permision.Category, permision.GetCRUD()));
                    }
                }
            }

            var Identities = new ClaimsIdentity(Claims);
            var ClaimPrinc = new ClaimsPrincipal(Identities);
            HttpContext.SignInAsync(ClaimPrinc);
        }
    }
}
