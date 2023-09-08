namespace ChainDoku.Services;

public class GridState
{
    public int? ActiveRow { get; set; }
    public int? ActiveColumn { get; set; }
    public int? ActiveValue { get; set; }

    public void Clear()
    {
        ActiveRow = null;
        ActiveColumn = null;
        ActiveValue = null;
    }
}
