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

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpGet]
        public IActionResult Login() => View();

        public async Task<IActionResult> Logout()
        {
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
                    return LocalRedirect(form.ReturnUrl ?? "/");
                }

            }
            return View(form);
        }
    }
}
