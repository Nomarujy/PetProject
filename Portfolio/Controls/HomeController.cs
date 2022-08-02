using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Data.Database.ContactService;

namespace Portfolio.Controls
{
    public class HomeController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger logger;

        public HomeController(IContactRepository databaseContext, ILogger<HomeController> Logger)
        {
            _contactRepository = databaseContext;
            logger = Logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.Add(contact);
                logger.LogInformation("Получено сообщение от {HttpContext}", HttpContext.Request.Host.Value);
                return Redirect("/");
            }

            return View("ModelError", ModelState);
        }

        [HttpGet]
        public IActionResult Messages(bool Descending = false, int Page = 0, int Count = 10)
        {
            Contact[] result;

            if (Descending) result = _contactRepository.GetLast(Page, Count);
            else result = _contactRepository.GetFirst(Page, Count);

            return View(result);
        }
    }
}
