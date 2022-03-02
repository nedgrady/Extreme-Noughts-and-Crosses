using ExtremeNoughtsAndCrosses.GameState;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;
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

            gameStateController.PlaceToken(1, 1, Token.X);

            gameStateStoreUnderTest.GameState.Should().BeEquivalentTo(
                new Token[,]
                {
                    {Token.Empty, Token.Empty, Token.Empty},
                    {Token.Empty, Token.X, Token.Empty},
                    {Token.Empty, Token.Empty, Token.Empty}
                });
        }

        [Fact]
        public void InTheTopLeftUpdatesTheGameState()
        {
            var gameStateStoreUnderTest = new GameStateStore();
            var gameStateController = new GameStateController(gameStateStoreUnderTest);

            gameStateController.PlaceToken(0, 0, Token.X);

            gameStateStoreUnderTest.GameState.Should().BeEquivalentTo(
                new Token[,]
                {
                    {Token.X, Token.Empty, Token.Empty},
                    {Token.Empty, Token.Empty, Token.Empty},
                    {Token.Empty, Token.Empty, Token.Empty}
                });
        }

        [Fact]
        public void InVariousPlacesIsReflectedByTheGameState()
        {
            var gameStateStoreUnderTest = new GameStateStore();
            var gameStateController = new GameStateController(gameStateStoreUnderTest);

            gameStateController.PlaceToken(0, 0, Token.X);
            gameStateController.PlaceToken(1, 1, Token.O);

            gameStateStoreUnderTest.GameState.Should().BeEquivalentTo(
                new Token[,]
                {
                    {Token.X, Token.Empty, Token.Empty},
                    {Token.Empty, Token.O, Token.Empty},
                    {Token.Empty, Token.Empty, Token.Empty}
                });
        }

        [Fact]
        public async Task Returns200WhenValidMoveMade()
        {
            var client =
                new WebApplicationFactory<Program>()
                    .CreateClient();

            var result = await client.PlaceTokenInPosition(0, 0, Token.X);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdatesGameStateInCorrectPlace()
        {
            var gameStateStore = new GameStateStore();

            var client =
                new WebApplicationFactory<Program>()
                    .WithWebHostBuilder(thing =>
                        thing.ConfigureServices(services =>
                        {
                            services.Remove<GameStateStore>();
                            services.AddSingleton(gameStateStore);
                        }))
                    .CreateClient();

            var result = await client.PlaceTokenInPosition(1, 2, Token.X);


            result.StatusCode.Should().Be(HttpStatusCode.OK);

            gameStateStore.GameState.Should().BeEquivalentTo(
                new Token[,]
                {
                    {Token.Empty, Token.Empty, Token.Empty},
                    {Token.Empty, Token.Empty, Token.X},
                    {Token.Empty, Token.Empty, Token.Empty}
                });
        }

        [Fact]
        public async Task InAnOccupiedPositionReturnsUnprocessableEntity()
        {
            var gameStateStore = new GameStateStore();

            gameStateStore.PlaceToken(0, 0, Token.X);

            var client =
                new WebApplicationFactory<Program>()
                    .WithWebHostBuilder(thing =>
                        thing.ConfigureServices(services =>
                        {
                            services.Remove<GameStateStore>();
                            services.AddSingleton(gameStateStore);
                        }))
                    .CreateClient();

            var result = await client.PlaceTokenInPosition(0, 0, Token.O);

            result.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

            gameStateStore.GameState.Should().BeEquivalentTo(
                new Token[,]
                {
                    {Token.X, Token.Empty, Token.Empty},
                    {Token.Empty, Token.Empty, Token.Empty},
                    {Token.Empty, Token.Empty, Token.Empty}
                });
        }

        [Fact]
        public async Task ThatAreEmptyReturnsBadRequest()
        {
            var client =
                new WebApplicationFactory<Program>()
                    .CreateClient();

            var result = await client.PlaceTokenInPosition(1, 1, Token.Empty);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task OutOfTurnOnTheFirstTurnReturnsUnprocessableEntity()
        {
            var client =
                new WebApplicationFactory<Program>()
                    .CreateClient();

            var result = await client.PlaceTokenInPosition(0, 0, Token.O);

            result.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task OutOfTurnOrderReturnsUnprocessableEntity()
        {
            var client =
                new WebApplicationFactory<Program>()
                    .CreateClient();

            await client.PlaceTokenInPosition(0, 0, Token.X);
            var result = await client.PlaceTokenInPosition(1, 0, Token.X);

            result.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}
