using System;
using System.Linq;
using System.Threading.Tasks;
using MicroMessager.Entites;
using MicroMessager.MessagerServer.Data;
using MicroMessager.MessagerServer.Dtos;
using MicroMessager.SDKs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;

namespace MicroMessager.MessagerServer.Controllers
{
    [ApiController]
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly TrackerService _trackerService;
        private readonly MessagerService _messagerService;

        public ChatController(
            ApplicationDbContext dbContext, 
            IConfiguration configuration, 
            TrackerService trackerService,
            MessagerService messagerService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _trackerService = trackerService;
            _messagerService = messagerService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromForm] SendMessageDto dto)
        {
            var message = new Message()
            {
                SenderUserId = dto.SenderUserId,
                ReceiverUserId = dto.ReceiverUserId,
                Content = dto.Content,
            };

            var server = await _trackerService.GetCurrentClientServerByUserId(dto.ReceiverUserId);

            if (_configuration["MyIP"] != server.ServerIp)
            {
                await _messagerService.PassThroughMessage(server.ServerIp, message);

                return Json(new { });
            }

            await _dbContext.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            return Json(new { });
        }

        [HttpGet]
        public async Task<IActionResult> GetUnreadMessages([FromQuery] int userId)
        {
            var messages = await _dbContext
                .Messages
                .Where(q => q.ReceiverUserId == userId)
                .OrderByDescending(o => o.CreatedTimeStamp)
                .AsNoTracking()
                .ToListAsync();

            foreach (var message in messages)
            {
                _dbContext.Remove(message);
            }

            await _dbContext.SaveChangesAsync();

            return Json(messages);
        }
    }
}