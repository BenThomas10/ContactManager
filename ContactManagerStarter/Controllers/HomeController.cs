using Microsoft.AspNetCore.Mvc;

namespace ContactManagerStarter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
