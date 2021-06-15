using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AIMLChatBot.Models.Base
{
    [DataContract]
    abstract public class BaseRequest
    {
        public string ContentType { get; set; }
        public string Authorization { get; set; }
        public string Language { get; set; }
        public string SourceId { get; set; }
    }
}
