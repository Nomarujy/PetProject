using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.ContactService;
using Portfolio.Models;

namespace Portfolio.Controls
{
    public class HomeController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger logger;

        public HomeController(IContactRepository databaseContext, ILogger<HomeController> Logger)
        {
            logger = Logger;
            _contactRepository = databaseContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactWithMe contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.Add(contact);
                logger.LogInformation("Получено сообщение от {0}", HttpContext.Request.Host.Value);
                return Ok();
            }

            return View("ModelError", ModelState);
        }

        [HttpGet]
        public IActionResult Messages(bool Descending = false, int count = 10)
        {
            ContactWithMe[] result;

            if (Descending) result = _contactRepository.GetLast(count);
            else result = _contactRepository.GetFirst(count);

            return View(result);
        }
    }
}
