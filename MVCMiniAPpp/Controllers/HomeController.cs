using Microsoft.AspNetCore.Mvc;

namespace MVCMiniApp
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}