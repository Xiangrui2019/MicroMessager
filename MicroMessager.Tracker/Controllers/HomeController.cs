using Microsoft.AspNetCore.Mvc;

namespace MicroMessager.Tracker.Controllers
{
    public class HomeController : Controller
    {
        // 检查服务器是否还活着
        [HttpGet]
        [Route("/ping")]
        public IActionResult Ping()
        {
            return Json(new {});
        }
    }
}