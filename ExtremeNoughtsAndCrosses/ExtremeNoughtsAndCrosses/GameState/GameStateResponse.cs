using System.Text.Json.Serialization;
using ExtremeNoughtsAndCrosses.Converters;

namespace ExtremeNoughtsAndCrosses.GameState
{
    public class GameStateResponse
    {
        [JsonConverter(typeof(TwoDimensionalToJaggedArrayConverter))]
        public bool?[,] GameState { get; set; }
    }
}