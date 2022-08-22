using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.StartPage;
using System.Security.Claims;

namespace Portfolio.Controls
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext database;

        public HomeController(ApplicationDbContext dbContext)
        {
            database = dbContext;
        }


        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                MessageModel model = new()
                {
                    Name = User.Identity?.Name ?? "",
                    Email = User.FindFirstValue(ClaimTypes.Email) ?? ""
                };

                return View(model);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(MessageModel model)
        {
            if (ModelState.IsValid)
            {
                database.Messages.Add(model);
            }

            return View(model);
        }
    }
}
