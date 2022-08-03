using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Authorization;
using Portfolio.Data.Database.AccountService;
using Portfolio.Models.Authorization;
using System.Security.Claims;

namespace Portfolio.Controls
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository database;
        private readonly IPasswordEncryptor passwordEncryptor;
        private readonly ILogger logger;

        public AccountController(IAccountRepository database, IPasswordEncryptor passwordEncryptor, ILogger<AccountController> logger)
        {
            this.database = database;
            this.passwordEncryptor = passwordEncryptor;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult AccessDenied()
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

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterForm userForm)
        {
            if (ModelState.IsValid == false) return BadRequest(ModelState.Values);

            User user = new(userForm, passwordEncryptor);

            database.AddUser(user);
            logger.LogInformation("Registered new user {user} by IP: {IP}", user.Email, HttpContext?.Connection.RemoteIpAddress);

            SignInUser(user);
            return LocalRedirect("/");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(LoginForm userForm, string? returnUrl)
        {
            User? user = database.GetUserWithRole(userForm.Email);

            if (user == null) return BadRequest("UserNotFound");

            if (passwordEncryptor.PasswordEqual(userForm.Password, user.Password) == false)
            {
                logger.LogInformation("Failed login atempt in {user} by IP: {IP}", user.Email, HttpContext?.Connection.RemoteIpAddress);
                return BadRequest();
            }

            SignInUser(user!);
            return LocalRedirect(returnUrl ?? "/");
        }

        private void SignInUser(User user)
        {
            Permision?[] permisions = database.GetPermisionse(user.RoleId);

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

                if (permisions != null)
                {
                    foreach (var permision in permisions)
                    {
                        Claims.Add(new Claim(permision!.Category, permision.GetCRUD()));
                    }
                }
            }

            var Identities = new ClaimsIdentity(Claims, "Cookies");
            var ClaimPrinc = new ClaimsPrincipal(Identities);
            HttpContext.SignInAsync(ClaimPrinc);
            logger.LogInformation("Sing in {user} by IP: {IP}", user.Email, HttpContext?.Connection.RemoteIpAddress);
        }
    }
}
