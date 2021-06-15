using AIMLChatBot.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AIMLChatBot.Enum;
using AIMLChatBot.Helper.CustomHelper;

namespace AIMLChatBot.Controllers.Base
{
    [ApiController]
    public abstract class BaseController : Controller
    {

        //create designated response
        protected virtual IActionResult ContentResponse(HttpStatusCode code, Response model)
        {
            IActionResult response;

            Response.StatusCode = (int)code;
            response = ApiHelper.Response((EnumReponseCode)code, result: model);

            return response;
        }
    }
}
