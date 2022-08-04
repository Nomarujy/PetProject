using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Contact.Repository;
using Portfolio.Models.Contact;

namespace Portfolio.Controls
{
    public class HomeController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger logger;

        public HomeController(IContactRepository databaseContext, ILogger<HomeController> logger)
        {
            _contactRepository = databaseContext;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult Messages(bool Descending = false, int Page = 0, int Count = 10)
        {
            ContactModel[] result;

            if (Descending) result = _contactRepository.GetLast(Page, Count);
            else result = _contactRepository.GetFirst(Page, Count);

            return View(result);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(ContactModel contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.Add(contact);
                logger.LogInformation("Geter message by {name}, IP: {IP}",
                    contact.Name, HttpContext.Connection.RemoteIpAddress);
                return Redirect("/");
            }

            return View("ModelError", ModelState);
        }
    }
}
