using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using AIMLChatBot.Enum;
using AIMLChatBot.Models.Common;
using Microsoft.AspNetCore.Mvc;


namespace AIMLChatBot.Helper.CustomHelper
{
    public class ApiHelper
    {
        private static string MSG_200 = "OK";
        private static string MSG_201 = "Created";
        private static string MSG_204 = "No Data";
        private static string MSG_400 = "Bad Request";
        private static string MSG_403 = "Unauthorized";
        private static string MSG_500 = "Internal Server Error";

        public static JsonResult Response(EnumReponseCode code, Response result = null)
        {

            result.ResponseCode = (HttpStatusCode)code;

            if (string.IsNullOrEmpty(result.Message))
            {
                //Get Message
                switch (code)
                {
                    case EnumReponseCode.Success:
                        result.Message = MSG_200;
                        break;
                    case EnumReponseCode.Created:
                        result.Message = MSG_201;
                        break;
                    case EnumReponseCode.BadRequest:
                        result.Message = MSG_400;
                        break;
                    case EnumReponseCode.Unauthorized:
                        result.Message = MSG_403;
                        break;
                    case EnumReponseCode.InternalServerError:
                        result.Message = MSG_500;
                        break;
                }
            }

            //Validations
            if (string.IsNullOrEmpty(result.Message) && result.Error == null && result.Error.Count > 0 && result.Result == null)
            {
                result.ResponseCode = HttpStatusCode.NoContent;
                result.Message = (!string.IsNullOrEmpty(result.Message) ? result.Message : MSG_204);
            }

            return new JsonResult(result);

        }
    }
}
