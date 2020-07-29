using System.Threading.Tasks;
using MicroMessager.Entites;
using MicroMessager.MessagerServer.Data;
using MicroMessager.SDKs.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroMessager.MessagerServer.Controllers
{
    public class ControlController : Controller
    {
        private readonly TrackerService _trackerService;
        private readonly MessagerService _messagerService;
        private readonly ApplicationDbContext _dbContext;

        public ControlController(
            TrackerService trackerService, 
            MessagerService messagerService, 
            ApplicationDbContext dbContext)
        {
            _trackerService = trackerService;
            _messagerService = messagerService;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> ServerChange(int userId)
        {
            var lastClientServer = await _trackerService.GetLastClientServerByUserId(userId);
            var messages = await _messagerService.GetUnreadMessages(lastClientServer.ServerIp, userId);

            foreach (var message in messages)
            {
                await _dbContext.Messages.AddAsync(message);
            }

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PassThroughMessage(Message message)
        {
            await _dbContext.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}