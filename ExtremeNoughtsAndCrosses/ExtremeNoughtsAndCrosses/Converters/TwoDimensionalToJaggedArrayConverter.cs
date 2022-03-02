using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ExtremeNoughtsAndCrosses.GameState;

namespace ExtremeNoughtsAndCrosses.Converters
{
    public class TwoDimensionalToJaggedArrayConverter : JsonConverter<Token[,]>
    {
        public override Token[,]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Token[,] { };
        }

        public override void Write(Utf8JsonWriter writer, Token[,] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            for (var x = 0; x < value.GetLength(0); x++)
            { 
                writer.WriteStartArray();
                
                for (var y = 0; y < value.GetLength(1); y++)
                {
                    var currentItem = value[x, y];

                    if (currentItem == Token.X)
                    {
                        writer.WriteStringValue("X");
                    }
                    else if (currentItem == Token.O)
                    {
                        writer.WriteStringValue("O");
                    }
                    else
                    {
                        writer.WriteStringValue("Empty");
                    }
                }

                writer.WriteEndArray();
            }

            writer.WriteEndArray();
        }
    }
}