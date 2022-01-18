using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExtremeNoughtsAndCrosses.Converters
{
    public class TwoDimensionalToJaggedArrayConverter : JsonConverter<bool?[,]>
    {
        public override bool?[,]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new bool?[,] { };
        }

        public override void Write(Utf8JsonWriter writer, bool?[,] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            for (var x = 0; x < value.GetLength(0); x++)
            { 
                writer.WriteStartArray();
                
                for (var y = 0; y < value.GetLength(1); y++)
                {
                    var currentItem = value[x, y];

                    if (currentItem.HasValue)
                    {
                        writer.WriteBooleanValue(currentItem.Value);
                    }
                    else
                    {
                        writer.WriteNullValue();
                    }
                }

                writer.WriteEndArray();
            }

            writer.WriteEndArray();
        }
    }
}