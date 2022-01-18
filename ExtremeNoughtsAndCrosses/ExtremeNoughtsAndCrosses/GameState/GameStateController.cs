using Microsoft.AspNetCore.Mvc;

namespace ExtremeNoughtsAndCrosses.GameState
{
    [ApiController]
    [Route("GameState")]
    public class GameStateController : ControllerBase
    {
        private readonly GameStateStore _gameStateStore;

        public GameStateController(GameStateStore gameStateStore)
        {
            _gameStateStore = gameStateStore;
        }

        [HttpGet]
        public GameStateResponse Get()
        {
            var gamesStateResponse = new GameStateResponse
            {
                GameState = _gameStateStore.GameState
            };

            return gamesStateResponse;
        }

        public void PlaceToken(int xPosition, int yPosition, bool tokenToPlace)
        {
            _gameStateStore.PlaceToken(xPosition, yPosition, tokenToPlace);
        }
    }
}

