using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.AIMLBot.Utils
{

    /// <summary>
    /// Denotes what part of the input path a node represents.
    /// 
    /// Used when pushing values represented by wildcards onto collections for
    /// the star, thatstar and topicstar AIML values.
    /// </summary>
    public enum MatchState
    {
        UserInput,
        That,
        Topic
    }
}
