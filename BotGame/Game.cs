using System.Collections.Generic;

namespace OpenTable.BotGame
{
    public class Game: IGameBoard {

        private Board  board;
        private List<IBot> bots; 
        private IBotFactory botFactory;

        public int Rows => board.Rows;

        public int Columns => board.Columns;

        public Game(int rows, int columns, IBotFactory botFactory)
        {
            if (rows<1 || columns< 1)
            {
                throw new BotGameException($"Invalid board size. minim size 1x1");
            }  

            this.board = new Board(rows, columns);
            this.bots = new List<IBot>();
            this.botFactory = botFactory;
        }

        public void CreateNexBot (Coordinates coordinates, CardinalPoint direction)
        {
            this.bots.Add(botFactory.CreateRegularBot(coordinates, direction, this));
        }

        public void MoveBot(Movement movement)
        {
            bots[bots.Count-1].Move(movement);
        }

        public IList<Position> GetBotPositions()
        {
            var positions = new List<Position>();
            foreach (var bot in bots)
            {
                positions.Add(bot.Position);
            }

            return positions;
        }        
    }
}