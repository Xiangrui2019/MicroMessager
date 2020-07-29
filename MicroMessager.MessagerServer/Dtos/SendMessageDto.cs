using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroMessager.MessagerServer.Dtos
{
    /**
     * 消息发送Dto
     */
    public class SendMessageDto
    {
        // 发送者用户Id
        public int SenderUserId { get; set; }
        // 接收者用户Id
        public int ReceiverUserId { get; set; }
        // 消息内容
        public string Content { get; set; }
    }
}
