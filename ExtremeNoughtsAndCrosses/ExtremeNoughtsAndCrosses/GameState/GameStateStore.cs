using System.Linq;

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

        public Token TurnToken { get; protected set; } = Token.X;

        public bool PlaceToken(int xPosition, int yPosition, Token tokenToPlace)
        {
            if (GameState[xPosition, yPosition] != Token.Empty)
            {
                return false;
            }

            if (tokenToPlace != TurnToken)
            {
                return false;
            }

            GameState[xPosition, yPosition] = tokenToPlace;
            TakeTurn();

            return true;
        }

        private void TakeTurn()
        {
            TurnToken = TurnToken == Token.O ? Token.X : Token.O;
        }
    }
}
