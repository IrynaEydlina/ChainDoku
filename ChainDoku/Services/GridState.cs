using Models.Helpers;

namespace ChainDoku.Services;

public class GridState
{
    public int? ActiveRow { get; set; }
    public int? ActiveColumn { get; set; }
    public int? ActiveValue { get; set; }
    public int? ActiveBlock { get; private set; }
    public bool IsNotesMode { get; private set; }

    public bool HasValue => ActiveValue.HasValue && ActiveValue.Value != 0;

    public void ToggleNotes()
    {
        IsNotesMode = !IsNotesMode;
    }

    public void SetCell(Models.Cell cell)
    {
        ActiveRow = cell.Row;
        ActiveColumn = cell.Column;
        ActiveValue = cell.Value;
        ActiveBlock = cell.Block;
    }

    public void Clear()
    {
        ActiveRow = null;
        ActiveColumn = null;
        ActiveValue = null;
        ActiveBlock = null;
    }
}
