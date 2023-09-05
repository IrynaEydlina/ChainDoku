namespace ChainDoku.Models;

public class SudokuGrid
{
    private IReadOnlyDictionary<(int, int), int> _subSquares = null;
    public IReadOnlyDictionary<(int, int), int> SubSquares
    {
        get
        {
            if (_subSquares != null)
            {
                return _subSquares;
            }
            var dic = new Dictionary<(int, int), int>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var startRow = i / 3;
                    var startColumn = j / 3;
                    dic.Add((i, j), startRow * 3 + startColumn);
                }
            }
            _subSquares = dic;
            return _subSquares;
        }
    }

    public SudokuGrid(List<SudokuCell> cells) => Cells = cells;

    public SudokuCell this[int row, int column] => Cells.FirstOrDefault(c => c.Row == row && c.Column == column);

    public List<SudokuCell> Cells { get; private set; }

    public void RemoveTempValues(int row, int column, int value)
    {
        RemoveTempValuesFromRow(row, value);
        RemoveTempValuesFromColumn(column, value);
        RemoveTempValuesFromSubGrid(row, column, value);
    }

    public void RemoveTempValuesFromRow(int row, int value)
    {
        foreach (var cell in Cells.Where(c => c.Row == row))
        {
            cell.TemporaryValues[value] = false;
        }
    }

    public void RemoveTempValuesFromColumn(int column, int value)
    {
        foreach (var cell in Cells.Where(c => c.Column == column))
        {
            cell.TemporaryValues[value] = false;
        }
    }

    public void RemoveTempValuesFromSubGrid(int row, int column, int value)
    {
        var gridIndex = SubSquares[(row, column)];
        foreach (var cell in Cells.Where(c => SubSquares[(c.Row, c.Column)] == gridIndex))
        {
            cell.TemporaryValues[value] = false;
        }
    }

    public void SetState(List<SudokuCell> cells)
    {
        Cells = cells;
    }
}