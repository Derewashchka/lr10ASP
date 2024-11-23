using Microsoft.AspNetCore.Mvc;
using lr10.Models;

namespace lr10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ConsultationModel());
        }

        [HttpPost]
        public IActionResult Index(ConsultationModel model)
        {
            _logger.LogInformation($"Отримано дані: {model.FullName}, {model.Email}, {model.ConsultationDate}");

            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Реєстрація успішна!";
                return View("Success", model);
            }

            foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError($"Помилка валідації: {modelError.ErrorMessage}");
            }

            return View(model);
        }
    }
}