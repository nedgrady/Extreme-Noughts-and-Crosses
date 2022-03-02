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

        public Token TurnToken { get; private set; } = Token.X;
        public Token Winner => GameState[0, 0] == Token.O && GameState[1, 0] == Token.O && GameState[2, 0] == Token.O ||
                               GameState[0, 1] == Token.O && GameState[1, 1] == Token.O && GameState[2, 1] == Token.O
                                ? Token.O : Token.X;

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
