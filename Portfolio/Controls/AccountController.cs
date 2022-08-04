using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Account.Encryptor;
using Portfolio.Data.Account.Repository;
using Portfolio.Models.Authorization;
using System.Security.Claims;

namespace Portfolio.Controls
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository database;
        private readonly ILogger logger;
        private readonly IPasswordEncryptor encryptor;

        public AccountController(IAccountRepository database, IPasswordEncryptor encryptor, ILogger<AccountController> logger)
        {
            this.database = database;
            this.encryptor = encryptor;
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
            return SignOut(new AuthenticationProperties() { RedirectUri="/"},"Cookies");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterForm userForm)
        {
            if (ModelState.IsValid == false) return BadRequest(ModelState.Values);

            User user = new(userForm, encryptor);

            database.AddUser(user);

            logger.LogInformation("Registered new user {user} by IP: {IP}", 
                user.Email, HttpContext.Connection.RemoteIpAddress);

            
            return Redirect("/");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(LoginForm userForm, string? returnUrl)
        {
            User? user = database.GetUserWithRole(userForm.Email);

            if (user == null) return BadRequest("UserNotFound");

            if (false == encryptor.PasswordEqual(userForm.Password, user.Password))
            {
                logger.LogInformation("Failed login atempt in {user} by IP: {IP}", 
                    user.Email, HttpContext.Connection.RemoteIpAddress);
                return BadRequest();
            }

            var Principal = database.GetClaimPrincipals(user);

            return SignIn(Principal, new AuthenticationProperties() 
            { AllowRefresh = true, RedirectUri=returnUrl??"/" }); ;
        }
    }
}
