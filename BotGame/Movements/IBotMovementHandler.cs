namespace OpenTable.BotGame
{
    public interface IBotMovementHandler
    {
        bool CanHandleMovement(Movement movement);
        Position HandleMovement(Position currentPosition, Movement movement);
    }

}