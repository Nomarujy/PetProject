using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Authentication.Entity;

namespace Portfolio.Controls
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index() => View(_userManager.Users.ToArray());

        [HttpGet]
        public IActionResult Info() => View(User.Claims);

        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToIndex();
        }

        private IActionResult RedirectToIndex()
        {
            return RedirectToAction("Index", "Admin", new { area = "" });
        }
    }
}
