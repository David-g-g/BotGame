namespace OpenTable.BotGame
{
    public interface IBotFactory
    {
        IBot CreateRegularBot(Coordinates coordinates, CardinalPoint Direction, IGameBoard board);
    }
}