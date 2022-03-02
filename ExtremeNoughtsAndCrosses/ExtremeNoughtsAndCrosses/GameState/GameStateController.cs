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
        public IActionResult PlaceToken([FromQuery] int xPosition, [FromQuery] int yPosition, [FromQuery] Token tokenToPlace)
        {
            var placed =_gameStateStore.PlaceToken(xPosition, yPosition, tokenToPlace);

            if (placed)
            {
                return Ok();
            }

            return UnprocessableEntity();
        }
    }
}

