namespace ExtremeNoughtsAndCrosses.GameState
{
    public class GameStateStore
    {
        public GameStateStore()
        {
            GameState = new Token[,]
            {
                { Token.Empty, Token.Empty, Token.Empty },
                { Token.Empty, Token.Empty, Token.Empty },
                { Token.Empty, Token.Empty, Token.Empty }
            };
        }

        public virtual Token[,] GameState { get; }

        public bool PlaceToken(int xPosition, int yPosition, Token tokenToPlace)
        {
            if (GameState[xPosition, yPosition] != Token.Empty)
            {
                return false;
            }

            GameState[xPosition, yPosition] = tokenToPlace;

            return true;
        }
    }
}
