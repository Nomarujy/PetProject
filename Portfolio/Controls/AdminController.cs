using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Authentication.FormModel;
using Portfolio.Models.Authentication.Entity;
using Portfolio.Models.Authentication.ViewModel;

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

        [HttpGet]
        public IActionResult Create() => View();

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user == null) return NotFound();

            EditViewModel viewModel = new() { Email = user.Email, UserName= user.Email };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterForm form)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    Email = form.Email,
                    UserName = form.UserName
                };

                var result = await _userManager.CreateAsync(user, form.Password);
                if (result.Succeeded)
                {
                    return RedirectToIndex();
                }
                else
                {
                    AddErrorsInModelState(result);
                }
            }

            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToIndex();
                    }
                    else
                    {
                        AddErrorsInModelState(result);
                    }

                }
            }

            return View(model);
        }


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

        private void AddErrorsInModelState(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }
    }
}
