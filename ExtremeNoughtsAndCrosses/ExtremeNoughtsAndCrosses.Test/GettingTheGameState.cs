using System;
using System.Net;
using System.Threading.Tasks;
using ExtremeNoughtsAndCrosses.GameState;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace ExtremeNoughtsAndCrosses.Test
{
    public class GettingTheGameState
    {
        [Fact]
        public void InitiallyReturnsAnEmptyBoards()
        {
            // Arrange
            var expectedGameState = new Token[,] { };
            var mockGameStateStore = new Mock<GameStateStore>();

            mockGameStateStore.Setup(
                    setupGameStore => setupGameStore.GameState)
                .Returns(expectedGameState);

            var gameStateController = new GameStateController(mockGameStateStore.Object);

            // Act
            var result = gameStateController.Get();

            // Assert
            Assert.Equal(expectedGameState, result.GameState);
        }

        [Fact]
        public void ReturnsGameStateFromGameStateStore()
        {
            // Arrange
            var expectedGameState = new Token[,]
            {
                {Token.Empty, Token.Empty, Token.Empty},
                {Token.Empty, Token.X, Token.Empty},
                { Token.Empty, Token.Empty, Token.Empty}
            };
            var mockGameStateStore = new Mock<GameStateStore>();

            mockGameStateStore.Setup(
                    setupGameStore => setupGameStore.GameState)
                .Returns(expectedGameState);
            
            var gameStateController = new GameStateController(mockGameStateStore.Object);

            // Act
            var result = gameStateController.Get();

            // Assert
            Assert.Equal(expectedGameState, result.GameState);
        }

        [Fact]
        public void GamesStartOffAsEmpty()
        {
            // Arrange
            var expectedGameState = new Token[,]
            {
                { Token.Empty, Token.Empty, Token.Empty},
                { Token.Empty, Token.Empty, Token.Empty},
                { Token.Empty, Token.Empty, Token.Empty }
            };
            var gameStateStore = new GameStateStore();

            // Act
            var result = gameStateStore.GameState;

            // Assert
            Assert.Equal(expectedGameState, result);
        }

        [Fact]
        public async Task Returns200Ok()
        {
            var client = 
                new WebApplicationFactory<Program>()
                    .CreateClient();
            // Act
            var result = await client.GetAsync("/GameState");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task ReturnsCorrectJson()
        {
            var client = 
                new WebApplicationFactory<Program>()
                    .CreateClient();

            var expectedGameState = new [,]
            {
                {Token.Empty, Token.Empty, Token.Empty},
                {Token.Empty, Token.Empty, Token.Empty},
                {Token.Empty, Token.Empty, Token.Empty}
            };

            // Act
            var result = await client.GetAsync("/GameState");

            var json = await result.Content.ReadAsStringAsync();

            var receivedGameStateResponse = JsonConvert.DeserializeObject<GameStateResponse>(json);

            // Assert
            receivedGameStateResponse.GameState.Should().BeEquivalentTo(expectedGameState);
        }

        [Fact]
        public async Task ReturnsCorrectJsonWhenVariousTokensArePresent()
        {
            var expectedGameState = new [,]
            {
                {Token.Empty, Token.Empty, Token.O},
                {Token.Empty, Token.X, Token.Empty},
                {Token.Empty, Token.Empty, Token.X}
            };

            var mockGameStateStore = new Mock<GameStateStore>();

            mockGameStateStore.Setup(
                    setupGameStore => setupGameStore.GameState)
                .Returns(expectedGameState);

            var client =
                new WebApplicationFactory<Program>()
                    .WithWebHostBuilder(thing =>
                        thing.ConfigureServices(services =>
                        {
                            services.Remove<GameStateStore>();
                            services.AddSingleton(mockGameStateStore.Object);
                        }))
                    .CreateClient();
            
            // Act
            var result = await client.GetAsync("/GameState");

            var json = await result.Content.ReadAsStringAsync();

            var receivedGameStateResponse = JsonConvert.DeserializeObject<GameStateResponse>(json);

            // Assert
            receivedGameStateResponse.GameState.Should().BeEquivalentTo(expectedGameState);
        }

        [Fact]
        public async Task GameStateIsPersisted()
        {
            var expectedGameState = new [,]
            {
                {Token.X, Token.Empty, Token.Empty},
                {Token.Empty, Token.Empty, Token.Empty},
                {Token.Empty, Token.Empty, Token.Empty}
            };

            var client =
                new WebApplicationFactory<Program>()
                    .WithWebHostBuilder(_ => {})
                    .CreateClient();

            // Act
            await client.PostAsync("/GameState?xPosition=0&yPosition=0&tokenToPlace=X", null);

            var result = await client.GetAsync("/GameState");

            var json = await result.Content.ReadAsStringAsync();

            var receivedGameStateResponse = JsonConvert.DeserializeObject<GameStateResponse>(json);

            // Assert
            receivedGameStateResponse.GameState.Should().BeEquivalentTo(expectedGameState);
        }
    }
}