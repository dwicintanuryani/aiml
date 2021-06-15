using AIMLChatBot.Models;
using AIMLChatBot.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.Interface
{
    interface IChatBotInitialization
    {
        Task<Response> Initialization(Request request);
    }
}
