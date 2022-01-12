using ExtremeNoughtsAndCrosses.GameState;
using Moq;
using Xunit;

namespace ExtremeNoughtsAndCrosses.Test
{
    public class GettingTheGameState
    {
        [Fact]
        public void InitiallyReturnsAnEmptyBoards()
        {
            // Arrange
            var expectedGameState = new bool?[] { };
            var mockGameStateStore = new Mock<GameStateStore>();

            mockGameStateStore.Setup(
                    setupGameStore => setupGameStore.GameState)
                .Returns(expectedGameState);

            var gameStateController = new GameStateController(mockGameStateStore.Object);

            // Act
            var result = gameStateController.Get();

            // Assert
            Assert.Equal(new bool?[] { }, result);
        }

        [Fact]
        public void ReturnsGameStateFromGameStateStore()
        {
            // Arrange
            var expectedGameState = new bool?[]
            {
                null, null, null,
                null, true, null,
                null, null, null
            };
            var mockGameStateStore = new Mock<GameStateStore>();

            mockGameStateStore.Setup(
                    setupGameStore => setupGameStore.GameState)
                .Returns(expectedGameState);
            
            var gameStateController = new GameStateController(mockGameStateStore.Object);

            // Act
            var result = gameStateController.Get();

            // Assert
            Assert.Equal(expectedGameState, result);
        }
    }
}