using AIMLChatBot.Controllers.Base;
using AIMLChatBot.Enum;
using AIMLChatBot.Helper.Extension;
using AIMLChatBot.Interface;
using AIMLChatBot.Models;
using AIMLChatBot.Models.Common;
using AIMLChatBot.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AIMLChatBot.Controllers
{


    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class ChatBotController : BaseController
    {

        private IChatBotInitialization service { get; set; }
        public ChatBotController(IServiceProvider serviceProvider)
        {
            this.service = serviceProvider.GetRequiredService<IChatBotInitialization>();
        }


        [HttpPost, Route("Request")]
        public async Task<IActionResult> ChatBotRequest ([FromBody] Request request)
        {
            var response = new Response();
            request.Language = Request.GetLanguageCultures();

            try
            {
                if (!ModelState.IsValid)
                {
                    response.ResponseCode = HttpStatusCode.BadRequest;
                    return ContentResponse(response.ResponseCode, response);
                }
                else
                {
                    var result = await Task.Run(() => service.Initialization(request));
                    return ContentResponse(result.ResponseCode, result);
                }
            }
            catch (Exception)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                return ContentResponse(response.ResponseCode, response);
            }            
        }
    }
}
