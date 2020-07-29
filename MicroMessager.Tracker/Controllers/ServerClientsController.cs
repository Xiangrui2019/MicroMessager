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
    public class ServerClientsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ServerClientsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // 跟踪信息更新, 跟踪用户现在在哪个服务器
        [HttpPost]
        public async Task<IActionResult> Track(CreateServerClientDto dto)
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

            return Ok();
        }

        // 获取用户所在的当前服务器
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

            return Ok(serverClient.Server);
        }
        
        // 获取用户所在的上一个服务器
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

            return Ok(serverClient.Server);
        }
    }
}
