using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroMessager.Entites;
using MicroMessager.Tracker.Data;
using MicroMessager.Tracker.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace MicroMessager.Tracker.Controllers
{
    [ApiController]
    [Route("{controller}/{action}")]
    public class ServerClientController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ServerClientController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] CreateServerClientDto dto)
        {
            var server = await _dbContext
                .Servers
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.ServerIp == dto.ServerIp);

            var serverClient = new ServerClient()
            {
                UserId = dto.UserId,
                ServerId = server.Id,
            };

            await _dbContext.AddAsync(serverClient);
            await _dbContext.SaveChangesAsync();

            return Json(new {});
        }

        [HttpGet]
        public async Task<IActionResult> CurrentServerByUserId([FromQuery] int userId)
        {
            var serverClients = await _dbContext
                .ServerClients
                .Where(q => q.UserId == userId)
                .Include(i => i.Server)
                .OrderByDescending(x => x.CreatedTimestamp)
                .AsNoTracking()
                .ToListAsync();

            var serverClient = serverClients[0];

            return Json(serverClient.Server);
        }

        [HttpGet]
        public async Task<IActionResult> LastServerByUserId([FromQuery] int userId)
        {
            var serverClients = await _dbContext
                .ServerClients
                .Where(q => q.UserId == userId)
                .Include(i => i.Server)
                .OrderByDescending(x => x.CreatedTimestamp)
                .AsNoTracking()
                .ToListAsync();

            var serverClient = serverClients[1];

            return Json(serverClient.Server);
        }
    }
}
