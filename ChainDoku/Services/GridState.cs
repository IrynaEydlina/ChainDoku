using System.Data.Common;

namespace ChainDoku.Services;

public class GridState
{
    public int? ActiveRow { get; set; }
    public int? ActiveColumn { get; set; }
    public int? ActiveValue { get; set; }

    public bool HasValue => ActiveValue.HasValue && ActiveValue.Value != 0;

    public int? Block => HasValue ? ActiveRow / 3 * 3 + ActiveColumn / 3 : null;

    public void Clear()
    {
        ActiveRow = null;
        ActiveColumn = null;
        ActiveValue = null;
    }
}
