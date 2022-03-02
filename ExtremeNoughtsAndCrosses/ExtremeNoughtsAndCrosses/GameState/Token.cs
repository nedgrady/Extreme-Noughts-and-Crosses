using System.Text.Json.Serialization;

namespace ExtremeNoughtsAndCrosses.GameState
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Token
    {
        Empty,
        X,
        O
    }
}