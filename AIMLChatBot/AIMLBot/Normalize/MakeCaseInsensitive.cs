using AIMLChatBot.AIMLBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AIMLChatBot.AIMLBot.Normalize
{
    public class MakeCaseInsensitive : TextTransformation
    {

        public MakeCaseInsensitive(AIMLBot bot) : base(bot)
        { }

        public MakeCaseInsensitive(AIMLBot bot, string inputString) : base(bot, inputString)
        { }
       
        public MakeCaseInsensitive(AIMLBot bot, string inputString, string lang) : base(bot, inputString, lang) 
        { }

        protected override string ProcessChange()
        {
            return this.inputString.ToUpper();
        }

        /// <summary>
        /// An ease-of-use static method that re-produces the instance transformation methods
        /// </summary>
        /// <param name="input">The string to transform</param>
        /// <returns>The resulting string</returns>
        public static string TransformInput(string input)
        {
            return input.ToUpper();
        }

    }
}
