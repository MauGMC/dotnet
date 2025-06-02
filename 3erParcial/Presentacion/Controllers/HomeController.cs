using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Presentacion.Models;

namespace Presentacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            return View();
        }

        public IActionResult Privacy()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
