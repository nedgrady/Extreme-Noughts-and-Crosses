using ExtremeNoughtsAndCrosses.GameState;
using FluentAssertions;
using Xunit;

namespace ExtremeNoughtsAndCrosses.Test
{
    public class PlacingTokens
    {
        [Fact]
        public void InTheCentreUpdatesTheGameState()
        {
            var gameStateStoreUnderTest = new GameStateStore();
            var gameStateController = new GameStateController(gameStateStoreUnderTest);

            gameStateController.PlaceToken(1, 1, true);

            gameStateStoreUnderTest.GameState.Should().BeEquivalentTo(
                new bool?[,]
                {
                    {null, null, null},
                    {null, true, null},
                    {null, null, null}
                });
        }

        [Fact]
        public void InTheTopLeftUpdatesTheGameState()
        {
            var gameStateStoreUnderTest = new GameStateStore();
            var gameStateController = new GameStateController(gameStateStoreUnderTest);

            gameStateController.PlaceToken(0, 0, true);

            gameStateStoreUnderTest.GameState.Should().BeEquivalentTo(
                new bool?[,]
                {
                    {true, null, null},
                    {null, null, null},
                    {null, null, null}
                });
        }

        [Fact]
        public void InVariousPlacesIsReflectedByTheGameState()
        {
            var gameStateStoreUnderTest = new GameStateStore();
            var gameStateController = new GameStateController(gameStateStoreUnderTest);

            gameStateController.PlaceToken(0, 0, true);
            gameStateController.PlaceToken(1, 1, false);

            gameStateStoreUnderTest.GameState.Should().BeEquivalentTo(
                new bool?[,]
                {
                    {true, null, null},
                    {null, false, null},
                    {null, null, null}
                });
        }
    }
}
