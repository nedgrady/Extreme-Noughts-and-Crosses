using ExtremeNoughtsAndCrosses.GameState;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExtremeNoughtsAndCrosses.Test
{
    internal static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PlaceTokenInPosition(this HttpClient client, int xPosition, int yPosition, Token tokenToPlace)
        {
            return client.PostAsync($"/GameState?xPosition={xPosition}&yPosition={yPosition}&tokenToPlace={tokenToPlace}", null);
        }

        public static Task<HttpResponseMessage> GetGameState(this HttpClient client)
        {
            return client.GetAsync("/GameState");
        }
    }
}
