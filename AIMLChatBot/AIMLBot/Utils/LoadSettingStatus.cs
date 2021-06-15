using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.AIMLBot.Utils
{
    public class LoadSettingStatus
    {
        //Load Setting Status
        #region Attributes
        /// <summary>
        /// The bot model this dictionary is associated with
        /// </summary>
        protected AIMLBot Bot;

        /// <summary>
        /// The bot load status for load model name settings
        /// </summary>
        public string LoadModelName { get; set; }

        /// <summary>
        /// The bot load status for load model status settings
        /// </summary>
        public bool LoadModelStatus { get; set; }

        /// <summary>
        /// The bot load status for load model message settings
        /// </summary>
        public string? LoadModelMessage { get; set; }

        /// <summary>
        /// The bot load status error for debugging and load message settings
        /// </summary>
        public List<string> LoadModelErrorSystem { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="bot"></param>
        public LoadSettingStatus (AIMLBot bot)
        {
            this.Bot = bot;
        }
        #endregion
    }
}
