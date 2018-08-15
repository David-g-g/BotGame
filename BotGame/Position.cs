namespace OpenTable.BotGame
{
    public struct Position
    {
        public Coordinates Coordinates { get; }
        public CardinalPoint Direction { get; }

        public Position(Coordinates coordinates, CardinalPoint direction)
        {
            this.Coordinates = coordinates;
            this.Direction = direction;
        }
    }

}