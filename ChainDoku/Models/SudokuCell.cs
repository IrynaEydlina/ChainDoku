namespace ChainDoku.Models;

public class SudokuCell
{
    public SudokuCell(int row, int column, int? value, bool isStatic)
    {
        Id = Guid.NewGuid();
        Row = row;
        Column = column;
        Value = value;
        IsStatic = isStatic;
        TemporaryValues = isStatic ? new() : Enumerable.Range(1, 9).ToDictionary(x => x, _ => true);
        IsBig = value.HasValue;
    }

    public Guid Id { get; }
    public int Row { get; }
    public int Column { get; }
    public int? Value { get; private set; }
    public bool IsStatic { get; init; }
    public bool IsBig { get; private set; }
    public Dictionary<int, bool> TemporaryValues { get; init; }

    public void AddTemp(int value)
    {
        if (IsStatic)
        {
            return;
        }

        Value = null;
        TemporaryValues[value] = !TemporaryValues[value];
        IsBig = false;
    }

    public void SetValue(int value)
    {
        if (IsStatic)
        {
            return;
        }

        Value = Value == value ? null : value;
        ClearTempValues();
        IsBig = true;
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
        foreach (var key in TemporaryValues.Keys)
        {
            TemporaryValues[key] = false;
        }
    }
}