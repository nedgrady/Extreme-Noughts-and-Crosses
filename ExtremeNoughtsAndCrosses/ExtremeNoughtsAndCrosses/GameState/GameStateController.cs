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

        [HttpPost]
        public void PlaceToken([FromQuery] int xPosition, [FromQuery] int yPosition, [FromQuery] bool tokenToPlace)
        {
            _gameStateStore.PlaceToken(xPosition, yPosition, tokenToPlace);
        }
    }
}

