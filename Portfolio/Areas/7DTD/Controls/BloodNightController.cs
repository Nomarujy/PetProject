using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas._7DTD.Data.BloodNightRepository;
using Portfolio.Areas._7DTD.Models;

namespace Portfolio.Areas._7DTD.Controls
{
    public class BloodNightController : Controller
    {
        private IBloodNightRepository repository;
        private ILogger logger;

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
        public IActionResult Update(int Day, int Hour, int MinsPerDay)
        {
            repository.InitServerTime(Day, Hour, MinsPerDay);

            logger.LogInformation("Area: 7DTD. Обновлены значения BloodNightRepository");
            return RedirectToAction("Index");
        }
    }
}
