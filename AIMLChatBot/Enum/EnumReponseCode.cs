using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.Enum
{
    public enum EnumReponseCode
    {
        [Description("OK")]
        Success = 200,
        [Description("Created")]
        Created = 201,
        [Description("No Content")]
        NoContent = 204,
        [Description("Bad Request")]
        BadRequest = 400,
        [Description("Unauthorized")]
        Unauthorized = 403,
        [Description("Internal Server Error")]
        InternalServerError = 500
    }
}
