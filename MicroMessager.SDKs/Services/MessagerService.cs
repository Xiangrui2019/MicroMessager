using System.Collections.Generic;
using System.Threading.Tasks;
using MicroMessager.Entites;
using MicroMessager.SDKs.Models;
using MicroMessager.SDKs.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MicroMessager.SDKs.Services
{
    public class MessagerService
    {
        private readonly IConfiguration _configuration;
        private readonly HTTPService _httpService;

        public MessagerService(IConfiguration configuration, HTTPService httpService)
        {
            _configuration = configuration;
            _httpService = httpService;
        }

        // 获取未读消息
        public async Task<List<Message>> GetUnreadMessages(string messagerIp, int userId)
        {
            var server = await _httpService.Get(
                new CoreUri(
                    messagerIp + "/Chat/GetUnreadMessages",
                    new
                    {
                        userId = userId,
                    }));

            var jResult = JsonConvert.DeserializeObject<List<Message>>(server);
            return jResult;
        }

        // 跨节点Passthrough消息
        public async Task PassThroughMessage(string messagerIp, Message message)
        {
            var uri = new CoreUri(messagerIp + "/Control/PassThroughMessage", new {});
            var form = new CoreUri(string.Empty, message);

            await _httpService.Post(uri, form);
        }
    }
}