namespace OpenTable.BotGame
{
    public class AdvanceMovementHandler : IBotMovementHandler
    {
        public Position HandleMovement(Position currentPosition, Movement movement)
        {
            var advanceX = currentPosition.Direction == CardinalPoint.E ? 1 : currentPosition.Direction == CardinalPoint.W ? -1 : 0;
            var advanceY = currentPosition.Direction == CardinalPoint.N ? 1 : currentPosition.Direction == CardinalPoint.S ? -1 : 0;

            var newCoordinates = new Coordinates { X = currentPosition.Coordinates.X + advanceX, Y = currentPosition.Coordinates.Y + advanceY };

            return new Position(newCoordinates, currentPosition.Direction);
        }

        public bool CanHandleMovement(Movement movement)
        {
            return movement == Movement.M;
        }
    }

}