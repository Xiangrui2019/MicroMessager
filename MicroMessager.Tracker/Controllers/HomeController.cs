using Microsoft.AspNetCore.Mvc;

namespace MicroMessager.Tracker.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/ping")]
        public IActionResult Ping()
        {
            return Json(new {});
        }
    }
}