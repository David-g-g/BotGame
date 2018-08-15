using System;
using System.Collections.Generic;

namespace OpenTable.BotGame
{
    public class RegularBot : IBot
    {
        private IGameBoard board { get; }
        public Position Position { get; private set; }
        private IList<IBotMovementHandler> botMovements;

        public RegularBot(Coordinates coordinates, CardinalPoint direction, IGameBoard board, IBotMovementHandlerFactory botMovementHandlerFactory)
        {
            this.board = board;

            if (!IsValidPosition(coordinates.X, coordinates.Y))
            {
                throw new BotGameException($"Position out of board range: x:{coordinates.X} y:{coordinates.Y}");
            }

            this.Position = new Position(coordinates, direction);
            this.botMovements = new List<IBotMovementHandler> { botMovementHandlerFactory.CreateMovement(BotMovement.Advance), 
                                                                botMovementHandlerFactory.CreateMovement(BotMovement.Rotate)};
        }

        public void Move(Movement movement)
        {
            foreach (var movementHandler in botMovements)
            {
                if (movementHandler.CanHandleMovement(movement))
                {
                    var newPosition = movementHandler.HandleMovement(this.Position, movement);

                    if (IsValidPosition(newPosition.Coordinates.X, newPosition.Coordinates.Y))
                    {
                        this.Position = newPosition;
                    }
                    else
                    {
                        throw new BotGameException($"Movement out of board range. new position x:{newPosition.Coordinates.X} y:{newPosition.Coordinates.Y}");
                    }
                }
            }
        }

        private bool IsValidPosition(int x, int y)
        {
            return x <= board.Columns && y <= board.Rows && x >= 0 && y >= 0;
        }
    }

}