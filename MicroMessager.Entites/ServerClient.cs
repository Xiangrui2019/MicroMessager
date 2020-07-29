using System;
using MicroMessager.Entites;

namespace MicroMessager.Entites
{
    public class ServerClient
    {
        public int Id { get; set; }

        // 用户Id
        public int UserId { get; set; }

        // 服务器Id
        public int ServerId { get; set; }
        // 服务器导航属性
        public Server Server { get; set; }

        // 服务器对应客户端创建时间
        public long CreatedTimestamp { get; set; } =
            (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
    }
}