namespace OpenTable.BotGame
{
    public interface IBot
    {
        Position Position { get; }
        void Move(Movement movement);
    }
}