using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.StartPage;
using Portfolio.Services.Repository;
using System.Security.Claims;

namespace Portfolio.Controls
{
    public class HomeController : Controller
    {
        private readonly IMessageRepository _repository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMessageRepository repository, ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
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

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(MessageModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(model);
                _logger.LogInformation("Added message by {u}", model.Name);
            }

            return View(model);
        }
    }
}
