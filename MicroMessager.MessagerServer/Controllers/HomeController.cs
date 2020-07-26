using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroMessager.MessagerServer.Controllers
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
