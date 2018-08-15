using System;

namespace OpenTable.BotGame
{
    public class BotGameException: Exception{

        public BotGameException()
        {
            
        }
        public BotGameException(string message)
            :base(message)
        {
            
        }

        public BotGameException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }

}