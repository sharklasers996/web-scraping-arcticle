using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class ScraperController : Controller
    {
        public IActionResult Table()
        {
            return View();
        }

        public IActionResult Link()
        {
            return View();
        }

        public IActionResult Button()
        {
            return View();
        }
    }
}
