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
    public class ServersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ServersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // 获取可用服务器列表
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
