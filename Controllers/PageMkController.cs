using Microsoft.AspNetCore.Mvc;

namespace TuProyecto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PageMkController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}