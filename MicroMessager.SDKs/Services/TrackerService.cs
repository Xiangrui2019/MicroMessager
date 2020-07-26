using System.Threading;
using System.Threading.Tasks;
using MicroMessager.Entites;
using MicroMessager.SDKs.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MicroMessager.SDKs.Services
{
    public class TrackerService
    {
        private readonly IConfiguration _configuration;
        private readonly HTTPService _httpService;

        public TrackerService(IConfiguration configuration, HTTPService httpService)
        {
            _configuration = configuration;
            _httpService = httpService;
        }

        // 获取当前的服务器
        public async Task<Server> GetCurrentClientServerByUserId(int userId)
        {
            var server = await _httpService.Get(
                new CoreUri(
                    _configuration.GetConnectionString("TrackerConnection") + "/ServerClient/CurrentServerByUserId",
                    new
                    {
                        userId = userId,
                    }));

            var jResult = JsonConvert.DeserializeObject<Server>(server);

            return jResult;
        }
        
        // 获取上一个服务器, 切服用
        public async Task<Server> GetLastClientServerByUserId(int userId)
        {
            var server = await _httpService.Get(
                new CoreUri(
                    _configuration.GetConnectionString("TrackerConnection") + "/ServerClient/LastServerByUserId", 
                    new
                    {
                        userId = userId,
                    }));

            var jResult = JsonConvert.DeserializeObject<Server>(server);

            return jResult;
        }
    }
}