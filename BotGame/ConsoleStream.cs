using System;

namespace OpenTable.BotGame
{
    public class ConsoleStream : IInputStream, IOutputStream
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }

    public interface IInputStream
    {
        string ReadLine();
    }

    public interface IOutputStream
    {
        void WriteLine(string text);
    }

}