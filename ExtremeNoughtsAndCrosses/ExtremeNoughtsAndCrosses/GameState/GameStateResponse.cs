using System.Text.Json.Serialization;
using ExtremeNoughtsAndCrosses.Converters;

namespace ExtremeNoughtsAndCrosses.GameState
{
    public class GameStateResponse
    {
        [JsonConverter(typeof(TwoDimensionalToJaggedArrayConverter))]
        public Token[,] GameState { get; set; }
    }
}