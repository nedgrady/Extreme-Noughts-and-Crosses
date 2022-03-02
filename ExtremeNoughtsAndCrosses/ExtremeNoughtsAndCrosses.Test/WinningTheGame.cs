using ExtremeNoughtsAndCrosses.GameState;
using FluentAssertions;
using Xunit;

namespace ExtremeNoughtsAndCrosses.Test
{
    public class WinningTheGame
    {
        [Fact]
        public void WithThreeXsAcrossTheTop()
        {
            var gameStateStoreUnderTest = new GameStateStore();

            gameStateStoreUnderTest.PlaceToken(0, 0, Token.X);
            gameStateStoreUnderTest.PlaceToken(0, 1, Token.O);
            gameStateStoreUnderTest.PlaceToken(1, 0, Token.X);
            gameStateStoreUnderTest.PlaceToken(1, 1, Token.O);
            gameStateStoreUnderTest.PlaceToken(2, 0, Token.X);

            gameStateStoreUnderTest.Winner.Should().Be(Token.X);
        }

        [Fact]
        public void WithThreeOsAcrossTheTop()
        {
            var gameStateStoreUnderTest = new GameStateStore();

            gameStateStoreUnderTest.PlaceToken(2, 2, Token.X);
            gameStateStoreUnderTest.PlaceToken(0, 0, Token.O);
            gameStateStoreUnderTest.PlaceToken(2, 1, Token.X);
            gameStateStoreUnderTest.PlaceToken(1, 0, Token.O);
            gameStateStoreUnderTest.PlaceToken(0, 2, Token.X);
            gameStateStoreUnderTest.PlaceToken(2, 0, Token.O);

            gameStateStoreUnderTest.Winner.Should().Be(Token.O);
        }

        [Fact]
        public void WithThreeOsAcrossTheMiddle()
        {
            var gameStateStoreUnderTest = new GameStateStore();

            gameStateStoreUnderTest.PlaceToken(2, 2, Token.X);
            gameStateStoreUnderTest.PlaceToken(0, 1, Token.O);
            gameStateStoreUnderTest.PlaceToken(0, 0, Token.X);
            gameStateStoreUnderTest.PlaceToken(1, 1, Token.O);
            gameStateStoreUnderTest.PlaceToken(1, 2, Token.X);
            gameStateStoreUnderTest.PlaceToken(2, 1, Token.O);

            gameStateStoreUnderTest.Winner.Should().Be(Token.O);
        }

        [Fact]
        public void WithThreeXsDownTheLeftSide()
        {
            var gameStateStoreUnderTest = new GameStateStore();

            gameStateStoreUnderTest.PlaceToken(0, 0, Token.X);
            gameStateStoreUnderTest.PlaceToken(1, 1, Token.O);
            gameStateStoreUnderTest.PlaceToken(0, 1, Token.X);
            gameStateStoreUnderTest.PlaceToken(1, 0, Token.O);
            gameStateStoreUnderTest.PlaceToken(0, 2, Token.X);

            gameStateStoreUnderTest.Winner.Should().Be(Token.O);
        }
    }
}
