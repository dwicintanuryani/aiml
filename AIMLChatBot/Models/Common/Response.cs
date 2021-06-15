using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AIMLChatBot.Models.Common
{
    public class Response
    {

        public string Version { get; set ; }
        public HttpStatusCode ResponseCode { get; set; }                
        public string Message { get; set; }
        public List<Messages> Error { get; set; }
        public dynamic Result { get; set; }
    }
}
