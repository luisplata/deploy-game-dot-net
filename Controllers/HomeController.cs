using Microsoft.AspNetCore.Mvc;

namespace DeployGame.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}