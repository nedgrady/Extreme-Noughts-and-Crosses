using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ExtremeNoughtsAndCrosses.GameState;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
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

        [Fact]
        public async Task Returns200WhenValidMoveMade()
        {
            var client =
                new WebApplicationFactory<Program>()
                    .CreateClient();

            var result = await client.PostAsync("/GameState?xPosition=0&yPosition=0&tokenToPlace=true", null);

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

            var result = await client.PostAsync("/GameState?xPosition=1&yPosition=2&tokenToPlace=true", null);


            result.StatusCode.Should().Be(HttpStatusCode.OK);

            gameStateStore.GameState.Should().BeEquivalentTo(
                new bool?[,]
                {
                    {null, null, null},
                    {null, null, true},
                    {null, null, null}
                });
        }
    }
}
