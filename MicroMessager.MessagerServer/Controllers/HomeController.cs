using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroMessager.MessagerServer.Controllers
{
    public class HomeController : Controller
    {
        // 检查服务器是否活着
        [HttpGet]
        [Route("/ping")]
        public IActionResult Ping()
        {
            return Json(new {});
        }
    }
}
