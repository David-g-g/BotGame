using System;
using System.Collections.Generic;

namespace OpenTable.BotGame
{
    public class RotateMovementHandler : IBotMovementHandler
    {
        private static Dictionary<int, CardinalPoint> degreesToCardinalPoint = new Dictionary<int, CardinalPoint> { { 0, CardinalPoint.N }, { 90, CardinalPoint.E }, { 180, CardinalPoint.S }, { 270, CardinalPoint.W } };
        private static Dictionary<CardinalPoint, int> cardinalPointToDegrees = new Dictionary<CardinalPoint, int> { { CardinalPoint.N, 0 }, { CardinalPoint.E, 90 }, { CardinalPoint.S, 180 }, { CardinalPoint.W, 270 } };

        public Position HandleMovement(Position currentPosition, Movement movement)
        {
            var originalDirectionInDegrees = ConvertCardinalPointToDegrees(currentPosition.Direction);
            var movementInDegrees = movement == Movement.L ? -90 : 90;

            var newDirectionInDegrees = (360+originalDirectionInDegrees + movementInDegrees) % 360;

            var newCardinalPoint = ConvertDegreesToCardinalPoint(newDirectionInDegrees);

             return new Position(currentPosition.Coordinates, newCardinalPoint);
        }

        public bool CanHandleMovement(Movement movement)
        {
            return movement == Movement.L || movement == Movement.R;
        }

        private static int ConvertCardinalPointToDegrees(CardinalPoint cardinalPoint)
        {
            if (cardinalPointToDegrees.ContainsKey(cardinalPoint))
            {
                return cardinalPointToDegrees[cardinalPoint];
            }

            throw new BotGameException($"Invalid cardinal point {cardinalPoint}");
        }

        private static CardinalPoint ConvertDegreesToCardinalPoint(int degrees)
        {
            if (degreesToCardinalPoint.ContainsKey(degrees))
            {
                return degreesToCardinalPoint[degrees];
            }

            throw new BotGameException($"Invalid degrees to cardinal point conversion. Degrees: {degrees}");
        }
    }

}