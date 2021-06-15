using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.AIMLBot.Utils
{
    /// <summary>
    /// Encapsulates all the required methods and attributes for any text transformation.
    /// 
    /// An input string is provided and various methods and attributes can be used to grab
    /// a transformed string.
    /// 
    /// The protected ProcessChange() method is abstract and should be overridden to contain 
    /// the code for transforming the input text into the output text.
    /// </summary>
    abstract public class TextTransformation
    {
        #region Attributes
        /// <summary>
        /// Instance of the input string
        /// </summary>
        protected string inputString;

        /// <summary>
        /// Instance of the input string
        /// </summary>
        protected string language;

        /// <summary>
        /// The bot that this transformation is connected with
        /// </summary>
        public AIMLBot bot;

        /// <summary>
        /// The transformed string
        /// </summary>
        public string OutputString
        {
            get { return this.Transform(); }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default ctor for used as part of late binding mechanism
        /// </summary>
        public TextTransformation()
        {
            this.bot = null;
            this.inputString = string.Empty;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="bot">The bot this transformer is a part of</param>
        public TextTransformation(AIMLBot bot)
        {
            this.bot = bot;
            this.inputString = string.Empty;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="bot">The bot this transformer is a part of</param>
        /// <param name="inputString">The input string to be transformed</param>
        public TextTransformation(AIMLBot bot, string inputString)
        {
            this.bot = bot;
            this.inputString = inputString;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="bot">The bot this transformer is a part of</param>
        /// <param name="inputString">The input string to be transformed</param>
        public TextTransformation(AIMLBot bot, string inputString, string language)
        {
            this.bot = bot;
            this.inputString = inputString;
            this.language = language;
        }

        #endregion

        #region Method
        /// <summary>
        /// Do a transformation on the supplied input string
        /// </summary>
        /// <param name="input">The string to be transformed</param>
        /// <returns>The resulting output</returns>
        public string Transform(string input)
        {
            this.inputString = input;
            return this.Transform();
        }

        /// <summary>
        /// Do a transformation on the string found in the InputString attribute
        /// </summary>
        /// <returns>The resulting transformed string</returns>
        public string Transform()
        {
            if (this.inputString.Length > 0)
            {
                return this.ProcessChange();
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        /// <summary>
        /// The method that does the actual processing of the text.
        /// </summary>
        /// <returns>The resulting processed text</returns>
        protected abstract string ProcessChange();
    }
}
