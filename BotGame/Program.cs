using System;
using OpenTable.BotGame;

namespace BotGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new ConsoleStream();
            var orchestrator = new GameOrchestrator(console, console);
            orchestrator.Start();
        }
    }
}
