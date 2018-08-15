using System;
using System.Collections.Generic;
using OpenTable.BotGame;

namespace BotGame
{
    public static class CommandParser
    {
        public static Coordinates ParseBoardSize(string input)
        {
            var inputComponents = input.Split(' ');

            if (inputComponents.Length < 2)
            {
                throw new BotGameException($"Invalid board size, command sould contain two integers");
            }

            int x;
            int y;

            if (!Int32.TryParse(inputComponents[0], out x))
            {
                throw new BotGameException("Invalid board size ${input[0]}");
            }

            if (!Int32.TryParse(inputComponents[1], out y))
            {
                throw new BotGameException("Invalid board size ${input[1]}");
            }

            return new Coordinates{X=x, Y=y};
        }

        public static Position ParseBotPosition(string input)
        {
            var inputComponents = input.Split(' ');

            if (inputComponents.Length < 3)
            {
                throw new BotGameException($"Invalid bot position, command sould contain two integers and a char");
            }

            int x;
            int y;
            string directionRaw = inputComponents[2].ToUpper();

            if (!Int32.TryParse(inputComponents[0], out x))
            {
                throw new BotGameException($"Invalid board size ${input[0]}");
            }

            if (!Int32.TryParse(inputComponents[1], out y))
            {
                throw new BotGameException($"Invalid board size ${input[1]}");
            }

            if (directionRaw != "N" && directionRaw != "S" && directionRaw != "E" && directionRaw != "W")
            {
                throw new BotGameException($"Invalid bot direction ${input[2]}");
            }

            var direction = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), directionRaw);

            return new Position(new Coordinates{X=x, Y=y}, direction);
        }

        public static IList<Movement> ParseMovements(string input)
        {
            var movements = new List<Movement>();
            
            foreach (var movement in input.ToUpper())
            {
                if (IsValidMovement(movement))
                {
                    movements.Add((Movement)Enum.Parse(typeof(Movement), movement.ToString()));
                }
            }

            return movements;
        }    

        public static bool IsValidMovement (char movement)
        {
            return movement=='L' || movement=='R' || movement=='M';
        }    
    }
}
