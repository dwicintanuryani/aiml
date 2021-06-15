using AIMLChatBot.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AIMLChatBot.Models
{

    [DataContract]
    public class Request : BaseRequest
    {
        [Required]
        [Display(Name = "BotID")]
        [DataMember(IsRequired = true)]
        public string BotID { get; set; }
        
        [Required]
        [Display(Name = "UserID")]
        [DataMember(IsRequired = true)]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "UserInput")]
        [DataMember(IsRequired = true)]
        public string  UserInput { get; set; }

        [Display(Name = "StartOn")]
        [DataMember(IsRequired = true)]
        public DateTime StartOn { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }
    }
}
