using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Models;

[JsonConverter(typeof(CellConverter))]
public sealed class Cell
{
    internal Cell()
    {
        
    }
    public Cell(int row, int column, int value)
    {
        Row = row;
        Column = column;
        Value = value;
        IsStatic = value != 0;
    }
    internal Cell(Cell cell)
    {
        Row = cell.Row;
        Column = cell.Column;
        Value = cell.Value;
        IsStatic = cell.IsStatic;
        Candidates = cell.Candidates.ToHashSet();
    }
    public int Row { get; set; }
    public int Column { get; set; }
    public int Value { get;  set; }
    public bool IsStatic { get; set; }
    public HashSet<int> Candidates { get; set; } = new HashSet<int>();
    public HashSet<int> InternalCandidates { get; set; } = new();
    public int Block => Row / 3 * 3 + Column / 3;

    public bool IsEmpty => !HasValue && !Candidates.Any();

    public bool HasValue => Value != 0;

    public void ToggleTemp(int value)
    {
        if (IsStatic)
        {
            return;
        }

        Value = default;
        if (Candidates.Contains(value))
        {
            Candidates.Remove(value);
        }
        else
        {
            Candidates.Add(value);
        }
    }

    public bool AddTemp(int value)
    {
        if (IsStatic)
        {
            return true;
        }

        var result = HasValue || !Candidates.Contains(value);
        Value = default;
        Candidates.Add(value);
        return result;
    }

    public void ToggleValue(int value)
    {
        if (IsStatic)
        {
            return;
        }

        Value = Value == value ? default : value;
        ClearTempValues();
    }

    public bool Clear()
    {
        if (IsStatic)
        {
            return false;
        }

        var result = HasValue || Candidates.Any();
        Value = default;
        ClearTempValues();
        return result;
    }

    private void ClearTempValues()
    {
        Candidates.Clear();
    }

    public bool SameRegion(Cell cell)
    {
        return cell.Row == Row || cell.Column == Column || cell.Block == Block;
    }

    public Cell Clone()
    {
        return new Cell(this);
    }
}