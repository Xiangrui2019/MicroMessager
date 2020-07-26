using System;

namespace MicroMessager.Entites
{
    public class Message
    {
        public int Id { get; set; }

        // 发送者用户ID
        public int SenderUserId { get; set; }
        // 接收者用户ID
        public int ReceiverUserId { get; set; }

        // 消息内容
        public string Content { get; set; }

        // 消息发送时间, 用来在客户端排序消息, 虽然没有卵用, 但是还是完整点好了
        public long CreatedTimeStamp { get; set; } =
            (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
    }
}