namespace ExtremeNoughtsAndCrosses.GameState
{
    public class GameStateStore
    {
        public GameStateStore()
        {
            GameState = new bool?[,]
            {
                { null, null, null },
                { null, null, null },
                { null, null, null }
            };
        }

        public virtual bool?[,] GameState { get; }

        public void PlaceToken(int xPosition, int yPosition, bool tokenToPlace)
        {
            GameState[xPosition, yPosition] = tokenToPlace;
        }
    }
}
