using AIMLChatBot.Helper.CustomHelper;
using AIMLChatBot.Interface;
using AIMLChatBot.Models;
using AIMLChatBot.Models.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AIMLChatBot.Service
{
    public class ChatBotInitialization : IChatBotInitialization
    {
        
        /// <summary>
        /// ctor
        /// </summary>
        public ChatBotInitialization()
        {

        }
        

        public async Task<Response> Initialization(Request request)
        {

            Response response = new Response();
            
            try
            { 
                Task task = await Task.Factory.StartNew(async () => RequestChatBot(request, ref response));
                return response;

            }
            catch (Exception)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                return response;
            }

        }

        private static void RequestChatBot(Request request, ref Response response) 
        {

            AIMLBot.AIMLBot bot = new AIMLBot.AIMLBot();
            
            
            bot.lang = request.Language;
            bot.AppSetings = ConfigHelper.Settings;

            //main setting
            bot.loadSettings();
            bot.loadAIMLFromFiles();

            if (!bot.LoadSettingStatus.LoadModelStatus)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.Message = bot.LoadSettingStatus.LoadModelMessage;
            }
            else
            {
                response.ResponseCode = HttpStatusCode.OK;
            }


            
        }
    }
}
