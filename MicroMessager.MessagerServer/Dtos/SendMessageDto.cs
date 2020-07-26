using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroMessager.MessagerServer.Dtos
{
    public class SendMessageDto
    {
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }

        public string Content { get; set; }
    }
}
