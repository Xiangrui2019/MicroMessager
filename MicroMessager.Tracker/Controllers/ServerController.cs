using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroMessager.Tracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace MicroMessager.Tracker.Controllers
{
    [ApiController]
    [Route("{controller}/{action}")]
    public class ServerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ServerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetServerList()
        {
            var servers = await _dbContext
                .Servers
                .AsNoTracking()
                .ToListAsync();

            return Json(servers);
        }
    }
}
