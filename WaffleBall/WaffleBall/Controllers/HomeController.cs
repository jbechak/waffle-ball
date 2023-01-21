using Microsoft.AspNetCore.Mvc;

namespace WaffleBall.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
