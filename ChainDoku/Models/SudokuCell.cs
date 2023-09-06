namespace ChainDoku.Models;

public class SudokuCell
{
    public SudokuCell(int row, int column, int? value, bool isStatic)
    {
        Row = row;
        Column = column;
        Value = value;
        IsStatic = isStatic;
        TemporaryValues = isStatic ? null : new();
    }

    public int Row { get; }
    public int Column { get; }
    public int? Value { get; private set; }
    public bool IsStatic { get; init; }
    public HashSet<int> TemporaryValues { get; init; }

    public bool IsEmpty => !Value.HasValue && !TemporaryValues.Any();

    public void AddTemp(int value)
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

    public void SetValue(int value)
    {
        if (IsStatic)
        {
            return;
        }

        Value = Value == value ? null : value;
        ClearTempValues();
    }

    public void Clear()
    {
        if (IsStatic)
        {
            return;
        }

        Value = null;
        ClearTempValues();
    }

    private void ClearTempValues()
    {
        TemporaryValues.Clear();
    }
}