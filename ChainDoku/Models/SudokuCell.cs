using System.Text.Json.Serialization;

namespace ChainDoku.Models;

public class SudokuCell
{
    public SudokuCell(int row, int column, int? value)
    {
        Row = row;
        Column = column;
        Value = value.HasValue && value != 0 ? value : null;
        IsStatic = Value.HasValue;
        TemporaryValues = IsStatic ? null : new();
    }
    [JsonPropertyName("r")]
    public int Row { get; }
    [JsonPropertyName("c")]
    public int Column { get; }
    [JsonPropertyName("v")]
    public int? Value { get; private set; }
    [JsonPropertyName("s")]
    public bool IsStatic { get; init; }
    [JsonPropertyName("t")]
    public HashSet<int> TemporaryValues { get; init; }

    [JsonIgnore]
    public bool IsEmpty => !Value.HasValue && !TemporaryValues.Any();

    public void ToggleTemp(int value)
    {
        if (IsStatic)
        {
            return;
        }

        Value = null;
        if (TemporaryValues.Contains(value))
        {
            TemporaryValues.Remove(value);
        }
        else
        {
            TemporaryValues.Add(value);
        }
    }

    public bool AddTemp(int value)
    {
        if (IsStatic)
        {
            return true;
        }

        var result = Value.HasValue || !TemporaryValues.Contains(value);
        Value = null;
        TemporaryValues.Add(value);
        return result;
    }

    public void ToggleValue(int value)
    {
        if (IsStatic)
        {
            return;
        }

        Value = Value == value ? null : value;
        ClearTempValues();
    }

    public bool Clear()
    {
        if (IsStatic)
        {
            return false;
        }

        var result = Value.HasValue || TemporaryValues.Any();
        Value = null;
        ClearTempValues();
        return result;
    }

    private void ClearTempValues()
    {
        TemporaryValues.Clear();
    }
}