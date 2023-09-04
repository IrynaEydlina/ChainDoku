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
        TemporaryValues = new();
    }

    public Guid Id { get; }

    public int Row { get; }
    public int Column { get; }
    public int? Value { get; private set; }
    public bool IsStatic { get; init; }
    public HashSet<int> TemporaryValues { get; init; }

    public void AddTemporaryValue(int value)
    {
        if (!TemporaryValues.Contains(value))
        {
            TemporaryValues.Add(value);
        }
    }

    public void RemoveTempararyValue(int value)
    {
        if (TemporaryValues.Contains(value))
        {
            TemporaryValues.Remove(value);
        }
    }

    public void SetValue(int value)
    {
        Value = Value == value ? null : value;
    }
}