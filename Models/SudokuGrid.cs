using System.Collections;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Models;

public sealed class SudokuGrid : IEnumerable<Cell>
{
    public ReadOnlyCollection<Region> Rows { get; private set; }
    public ReadOnlyCollection<Region> Columns { get; private set; }
    public ReadOnlyCollection<Region> Blocks { get; private set; }
    public ReadOnlyCollection<ReadOnlyCollection<Region>> Regions { get; private set; }

    private IEnumerable<Cell> _flatCollection;

    public SudokuGrid(Cell[,] grid)
    {
        Grid = grid;
        _flatCollection = InitFlatCollection();
        InitRegions();
    }

    private void InitRegions()
    {
        var rows = new Region[9];
        var columns = new Region[9];
        var blocks = new Region[9];
        for (int i = 0; i < 9; i++)
        {
            var cells = new Cell[9];
            int c;

            for (c = 0; c < 9; c++)
            {
                cells[c] = Grid[c, i];
            }
            rows[i] = new Region(cells);

            for (c = 0; c < 9; c++)
            {
                cells[c] = Grid[i, c];
            }
            columns[i] = new Region(cells);

            c = 0;
            int ix = i % 3 * 3;
            int iy = i / 3 * 3;
            for (int x = ix; x < ix + 3; x++)
            {
                for (int y = iy; y < iy + 3; y++)
                {
                    cells[c++] = Grid[x, y];
                }
            }
            blocks[i] = new Region(cells);
        }
        Rows = rows.AsReadOnly();
        Columns = columns.AsReadOnly();
        Blocks = blocks.AsReadOnly();
        Regions = new[]
        {
            Rows, Columns, Blocks
        }.AsReadOnly();
    }

    private IEnumerable<Cell> InitFlatCollection()
    {
        var result = new List<Cell>();
        for (int row = 0; row < Grid.GetLength(0); row++)
        {
            for (int column = 0; column < Grid.GetLength(1); column++)
            {
                result.Add(Grid[row, column]);
            }
        }
        return result;
    }

    public Cell[,] Grid { get; private set; }

    public Cell this[int row, int column] => Grid[row, column];

    public void SetState(Cell[,] grid)
    {
        Grid = grid;
    }

    public IEnumerator<Cell> GetEnumerator()
    {
        return _flatCollection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _flatCollection.GetEnumerator();
    }
}
