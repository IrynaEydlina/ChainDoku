using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models;

internal class CellConverter : JsonConverter<Cell>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return true;
    }

    public override Cell? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetInt64().ToString();
        return new Cell
        {
            Row = int.Parse(str[0].ToString()),
            Column = int.Parse(str[1].ToString()),
            Value = int.Parse(str[2].ToString()),
            IsStatic = bool.Parse(str[3].ToString()),
            Candidates = str[4..].Select(c => int.Parse(c.ToString())).ToHashSet()
        };
    }

    public override void Write(Utf8JsonWriter writer, Cell value, JsonSerializerOptions options)
    {

        long intValue = long.Parse($"{value.Row}{value.Column}{value.Value}{(value.IsStatic ? 1 : 0)}{string.Join("", value.Candidates)}");
        writer.WriteNumberValue(intValue);
    }
}
