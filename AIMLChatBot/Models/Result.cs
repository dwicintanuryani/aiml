using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.Models
{
    public class Result
    {
        public string BotID { get; set; }
        public string BotReply { get; set; }
        public string Content { get; set; }
        public DateTime ReplyOn  { get; set; }
    }
}
