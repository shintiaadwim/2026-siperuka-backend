using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.Converters;

public class TimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (TimeSpan.TryParse(value, out var timespan))
        {
            return timespan;
        }
        throw new JsonException($"Unable to convert \"{value}\" to TimeSpan.");
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(@"hh\:mm\:ss"));
    }
}
