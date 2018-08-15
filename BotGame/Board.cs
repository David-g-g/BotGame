namespace OpenTable.BotGame
{
    public class Board {
        public int Rows { get; private set;}
        public int Columns { get; private set;} 
        
        public Board (int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
        }
    }
}