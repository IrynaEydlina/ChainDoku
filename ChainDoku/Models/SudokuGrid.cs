namespace ChainDoku.Models;

public class SudokuGrid
{
    private const int Size = 9;
    public int[,] SubSquares { get; private set; }

    public SudokuGrid(SudokuCell[,] grid)
    {
        Grid = grid;
        Init();
    }

    private void Init()
    {
        SubSquares = new int[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                var startRow = i / 3;
                var startColumn = j / 3;
                SubSquares[i, j] = startRow * 3 + startColumn;
            }
        }
    }

    public SudokuCell[,] Grid { get; private set; }

    public SudokuCell this[int row, int column] => Grid[row, column];

    public void RemoveTempValues(int row, int column, int value)
    {
        var gridIndex = SubSquares[row, column];
        for (var i = 0; i < Size; i++)
        {
            for (var j = 0; j < Size; j++)
            {
                if (!Grid[i, j].IsStatic && (i == row || j == column || gridIndex == SubSquares[i, j]))
                {
                    Grid[i, j].TemporaryValues.Remove(value);
                }
            }
        }
    }

    public void SetState(SudokuCell[,] grid)
    {
        Grid = grid;
    }
}