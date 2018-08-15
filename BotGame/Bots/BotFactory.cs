namespace OpenTable.BotGame
{
    public class BotFactory : IBotFactory
    {
        private readonly IBotMovementHandlerFactory botMovementHandlerFactory;

        public BotFactory(IBotMovementHandlerFactory botMovementHandlerFactory)
        {
            this.botMovementHandlerFactory = botMovementHandlerFactory;
        }
        public IBot CreateRegularBot(Coordinates coordinates, CardinalPoint direction, IGameBoard board)
        {
            return new RegularBot(coordinates, direction, board, botMovementHandlerFactory);
        }
    }
}