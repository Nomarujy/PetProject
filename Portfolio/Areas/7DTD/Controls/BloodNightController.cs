using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas._7DTD.Models;
using Portfolio.Areas._7DTD.Services.Repository;

namespace Portfolio.Areas._7DTD.Controls
{
    [Area("7DTD")]
    public class BloodNightController : Controller
    {
        private readonly IBloodNightRepository repository;
        private readonly ILogger logger;

        public BloodNightController(IBloodNightRepository bloodNightRepository, ILogger<BloodNightController> Logger)
        {
            repository = bloodNightRepository;
            logger = Logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewData = repository.GetView();
            return View(viewData);
        }

        [HttpPost]
        public IActionResult Update(InputForm form)
        {
            if (ModelState.IsValid)
            {
                repository.InitServerTime(form.Day, form.Hour, form.MinsPerDay);

                logger.LogInformation("Updated servertime Day:{D}, Hour:{H}, MinsPerDay:{MPD}", form.Day, form.Hour, form.MinsPerDay);

                return RedirectToAction("Index", "BloodNight", new { area = "7DTD" });
            }

            return BadRequest();
        }
    }
}
