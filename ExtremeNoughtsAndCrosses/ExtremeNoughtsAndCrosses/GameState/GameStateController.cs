namespace ExtremeNoughtsAndCrosses.GameState
{
    public class GameStateController
    {
        private readonly GameStateStore _gameStateStore;

        public GameStateController(GameStateStore gameStateStore)
        {
            _gameStateStore = gameStateStore;
        }

        public bool?[] Get()
        {
            return _gameStateStore.GameState;
        }
    }
}

