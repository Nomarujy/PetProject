using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Authentication.Entity;
using Portfolio.Models.Authentication.FormModel;

namespace Portfolio.Controls
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet, Authorize]
        public async Task<IActionResult> Info()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("User: {user} logout", User.Identity?.Name);
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterForm form)
        {
            if (ModelState.IsValid)
            {
                User user = new() { Email = form.Email, UserName = form.UserName };
                var result = await _userManager.CreateAsync(user, form.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Зарегестрирован пользователь {userName}", user.UserName);
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(form);
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginForm form)
        {
            if (ModelState.IsValid)
            {
                var res = await _signInManager.PasswordSignInAsync(form.UserName, form.Password, form.RememberMe, true);

                if (res.Succeeded)
                {
                    _logger.LogInformation("Login success. User: {user}", form.UserName);
                    return LocalRedirect(form.ReturnUrl ?? "/");
                }
                else
                {
                    _logger.LogInformation("Login failed. User: {user}", form.UserName);
                }

            }
            return View(form);
        }
    }
}
