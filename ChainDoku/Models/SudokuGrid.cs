namespace ChainDoku.Models;

public class SudokuGrid
{
    public SudokuGrid(SudokuCell[,] grid)
    {
        Grid = grid;
    }

    public SudokuCell[,] Grid { get; private set; }

    public SudokuCell this[int row, int column] => Grid[row, column];

    public void SetState(SudokuCell[,] grid)
    {
        Grid = grid;
    }
}